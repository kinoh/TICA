#include <cstdlib>

#ifdef _MSC_VER
#pragma warning(disable: 4242 4350 4365 4514 4626 4640 4710 4711 4714 4820)
#endif
#include "Eigen/Core"
#include "Eigen/LU"
#include "Eigen/Eigenvalues"

#define __DLL extern "C" __declspec(dllexport)

using namespace std;
using namespace Eigen;

typedef Matrix<double, Dynamic, Dynamic, RowMajor> MatrixXdR;


void Covariance(const MatrixXd &A, MatrixXd &C)
{
	C = MatrixXd::Zero(A.rows(), A.rows());

	for (int k = 0; k < A.cols(); k++)
		C += A.col(k) * A.col(k).transpose();
	C /= (double)A.cols();
}

void MatrixSqrt(const MatrixXd &A, MatrixXd &S)
{
	SelfAdjointEigenSolver<MatrixXd> es(A);	// lower triangular part only referenced

	S = es.eigenvectors() * es.eigenvalues().unaryExpr(ptr_fun(static_cast<double (*)(double)>(&sqrt))).asDiagonal() * es.eigenvectors().transpose();
}

void Whitening(MatrixXd &X)
{
	MatrixXd Winv(X.rows(), X.rows());

	X = X.colwise() - X.rowwise().mean();

	Covariance(X, Winv);
	MatrixSqrt(Winv, Winv);	// Wz^-1 = Cov[centerized X] ^(1/2)

	for (int i = 0; i < X.cols(); i++)
		X.col(i) = Winv.colPivHouseholderQr().solve(X.col(i));
}

inline void set(double dst[], MatrixXd src)
{
	for (int i = 0; i < src.rows(); i++)
		for (int j = 0; j < src.cols(); j++)
			dst[i * src.cols() + j] = src(i, j);
}

__DLL void __stdcall Initialize(int dim, double w[])
{
	Map<MatrixXdR> W(w, dim, dim);

	W = MatrixXdR::Identity(dim, dim);
}

__DLL double __stdcall Learn(int dim, double w[], double data[], int count, bool whiten, double rate)
{
	Map<MatrixXdR> W(w, dim, dim);
	MatrixXd m = Map<MatrixXd>(data, dim, count);

	if (whiten)
		Whitening(m);

	double J = 0;
	for (int i = 0; i < m.cols(); i++)
	{
		VectorXd x = m.col(i);
		VectorXd u = W * x;
		VectorXd y = u.unaryExpr([](double z) { return 1.0 / (1 + exp(-z)); });
		W += rate * (MatrixXdR::Identity(dim, dim) + (VectorXd::Ones(dim) - 2 * y) * u.transpose()) * W;
		//J += log(abs((u.unaryExpr([](double z) { double p = exp(-z); return p * pow(1 + p, -2); }).asDiagonal() * W).determinant())) / m.cols();
	}

	return J;
}

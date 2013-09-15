#include "ICA.h"
#include <iostream>

ICA::ICA(int n_u, int n_x)
	: n_filter(n_u),
	n_pixel(n_x),
	weight(MatrixXd::Identity(n_u, n_x))
{
}

ICA::~ICA()
{
}

MatrixXd &ICA::Filter()
{
	return weight;
}

void ICA::Learn(MatrixXd &data, double rate)
{
	double J = 0;
	for (int i = 0; i < data.cols(); i++)
	{
		VectorXd x = data.col(i);
		VectorXd u = weight * x;
		VectorXd y = u.unaryExpr([](double z) { return 1.0 / (1 + exp(-z)); });
		weight += rate * (MatrixXd::Identity(n_filter, n_pixel) + (VectorXd::Ones(n_pixel) - 2 * y) * u.transpose()) * weight;
		J += log(abs((u.unaryExpr([](double z) { double p = exp(-z); return p * pow(1 + p, -2); }).asDiagonal() * weight).determinant()));
		if (i % 100 == 0)
		{
			std::cout << "Entropy : " << (J / 100) << std::endl;
			J = 0;
		}
	}
}

void ICA::Whitening(MatrixXd &X)
{
	MatrixXd Winv(X.rows(), X.rows());

	X = X.colwise() - X.rowwise().mean();

	Covariance(X, Winv);
	MatrixSqrt(Winv, Winv);	// Wz^-1 = Cov[centerized X] ^(1/2)

	for (int i = 0; i < X.cols(); i++)
		X.col(i) = Winv.colPivHouseholderQr().solve(X.col(i));
}

void ICA::Covariance(const MatrixXd &A, MatrixXd &C)
{
	C = MatrixXd::Zero(A.rows(), A.rows());

	for (int k = 0; k < A.cols(); k++)
		C += A.col(k) * A.col(k).transpose();
	C /= A.cols();
}

void ICA::MatrixSqrt(const MatrixXd &A, MatrixXd &S)
{
	SelfAdjointEigenSolver<MatrixXd> es(A);	// lower triangular part only referenced

	S = es.eigenvectors() * es.eigenvalues().unaryExpr(ptr_fun(static_cast<double (*)(double)>(&sqrt))).asDiagonal() * es.eigenvectors().transpose();
}

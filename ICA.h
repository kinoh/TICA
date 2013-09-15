#pragma once

#include <cstdlib>

#ifdef _MSC_VER
#pragma warning( disable: 4365 4626 4640 4714 )
#endif
#include "Eigen/Core"
#include "Eigen/LU"
#include "Eigen/Eigenvalues"
#ifdef _MSC_VER
#pragma warning( default: 4365 4626 4640 )
#endif

using namespace std;
using namespace Eigen;

class ICA
{
private:
	int			n_filter,	// number of filters
				n_pixel;	// image patch size
	MatrixXd	weight;

public:
	ICA(int n_u, int n_x);
	~ICA();
	MatrixXd &Filter();
	void Learn(MatrixXd &data, double rate);
	void Whitening(MatrixXd &X);

private:
	//ICA(ICA&);
	//ICA& operator =(const ICA&);
	void Covariance(const MatrixXd &A, MatrixXd &C);
	void MatrixSqrt(const MatrixXd &A, MatrixXd &S);
};

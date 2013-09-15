#include <iostream>
#include <iomanip>
#include <cmath>
#include <cstdlib>
#include <sstream>
#include <string>
#include <chrono>
#include <random>

#include "ICA.h"
#include "BitmapPicker.h"
#include "BitmapWriter.h"

#ifdef _MSC_VER
#pragma warning( disable: 4365 4626 4640 4714 )
#endif
#include "Eigen/Core"
#include "Eigen/Eigenvalues"
#ifdef _MSC_VER
#pragma warning( default: 4365 4626 4640 )
#endif

#define MeasureTime(CODE)	auto const __begin = std::chrono::system_clock::now(); \
							CODE \
							auto const __end = std::chrono::system_clock::now();
#define MeasuredTime		std::chrono::duration_cast<std::chrono::milliseconds>(__end - __begin).count()

using namespace std;
using namespace Eigen;

random_device rd;
mt19937 gen(rd());

inline void set(MatrixXd &m, int i, int j, double value)
{
	if (i >= 0 && i < m.rows())
		m(i, j) = value;
}

void ReadImage(MatrixXd &m, char dir[])
{
	int d = m.rows();
	int l = (int)sqrt(d);
	const int patch_per_img = 20;
	auto patch = new unsigned char[(unsigned int)d];

	for (int i = 0; i < m.cols() / patch_per_img; i++)
	{
		stringstream src;
		src << dir << "/" << setfill('0') << setw(5) << right << (i + 1) << ".bmp";

		BitmapPicker Picker(src.str());
		uniform_int_distribution<int> x_distr(0, Picker.Width() - l);
		uniform_int_distribution<int> y_distr(0, Picker.Height() - l);

		cout << "read " << src.str() << endl;

		for (int j = 0; j < patch_per_img; j++)
		{
			Picker.Pick(l, x_distr(gen), y_distr(gen), patch);
			for (int k = 0; k < d; k++)
				m(k, i * patch_per_img + j) = patch[k] / 127.5 - 1.0;
		}
	}
}

void GenerateDummy(MatrixXd &m)
{
	int d = m.rows();
	int l = (int)sqrt(d);
	uniform_int_distribution<int> x_distr(0, 2 * l - 1);
	uniform_int_distribution<int> dx_distr(-2, 2);

	m.setZero(m.rows(), m.cols());

	for (int i = 0; i < m.cols(); i++)
	{
		int x1, x2, y1 = 0, y2 = l - 1;

		x1 = x_distr(gen);
		x2 = x_distr(gen);
		if (x1 >= l)
		{
			y1 = x1 - l;
			x1 = l - 1;
		}
		if (x2 >= l)
		{
			y2 -= x2 - l;
			x2 = 0;
		}

		if (abs(x1 - x2) > abs(y1 - y2))
			for (int x = min(x1, x2); x <= max(x1, x2); x++)
			{
				int y = y1 + (x - x1) * (y2 - y1) / (x2 - x1);
				if (y < 0 || y >= l)
					break;
				m(x * l + y, i) = 0.8;
				//set(m, (x + dx_distr(gen)) * l + y + dx_distr(gen), i, 0.5);
				//set(m, (x + dx_distr(gen)) * l + y + dx_distr(gen), i, 0.2);
			}
		else if (abs(x1 - x2) < abs(y1 - y2))
			for (int y = min(y1, y2); y <= max(y1, y2); y++)
			{
				int x = x1 + (y - y1) * (x2 - x1) / (y2 - y1);
				if (x < 0 || x >= l)
					break;
				m(x * l + y, i) = 0.8;
				//set(m, (x + dx_distr(gen)) * l + y + dx_distr(gen), i, 0.5);
				//set(m, (x + dx_distr(gen)) * l + y + dx_distr(gen), i, 0.2);
			}
		else
			i--;
	}
}

int main(int argc, char *argv[])
{
	if (argc - 1 < 3)
	{
		cerr << "Too few arguments; execute as '***.exe <image directory> <patch width> <number of samples>'" << endl;
		exit(EXIT_FAILURE);
	}
	
	int l = std::atoi(argv[2]);
	int d = l * l;
	int n = std::atoi(argv[3]);

	if (n <= d)
	{
		cerr << "Too few samples; must be greater than dimention" << endl;
		exit(EXIT_FAILURE);
	}

	MatrixXd data(d, n);
	ReadImage(data, argv[1]);
	//GenerateDummy(data);

	ICA ica(d, d);
	//ica.Whitening(data);
	for (int i = 0; i < 21; i++)
		ica.Learn(data, 0.001);
	for (int i = 0; i < 3; i++)
		ica.Learn(data, 0.0005);
	for (int i = 0; i < 3; i++)
		ica.Learn(data, 0.0002);
	for (int i = 0; i < 3; i++)
		ica.Learn(data, 0.0001);

	auto patch = new unsigned char[(unsigned int)d];

	for (int i = 0; i < 100; i++)
	{
		for (int k = 0; k < d; k++)
			patch[k] = (unsigned char)(127.5 * (data(k, i) + 1.0));

		stringstream src;
		src << "./patch/" << setfill('0') << setw(3) << right << i << ".bmp";
		BitmapWriter::WriteImage(src.str(), patch, l, l);
	}
	for (int i = 0; i < ica.Filter().cols(); i++)
	{
		double max = ica.Filter().row(i).array().abs().maxCoeff();
		for (int k = 0; k < d; k++)
			patch[k] = (unsigned char)(127.5 * (ica.Filter()(i, k) / max + 1.0));

		stringstream src;
		src << "./filter/" << setfill('0') << setw(3) << right << i << ".bmp";
		BitmapWriter::WriteImage(src.str(), patch, l, l);
	}

	delete [] patch;

	return EXIT_SUCCESS;
}

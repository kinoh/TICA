#include "BItmapPicker.h"

BitmapPicker::BitmapPicker(string bmpfile)
{
	path = bmpfile;
	LoadImage();
}

BitmapPicker::~BitmapPicker()
{
	delete [] data;
}

int BitmapPicker::Width()
{
	return (int)header.width;
}
int BitmapPicker::Height()
{
	return (int)header.height;
}

void BitmapPicker::LoadImage()
{
	ifstream fin(path.c_str(), ios::in | ios::binary);
	unsigned char buffer[120];

	fin.read((char *)buffer, 18);
	header.size = ConvLE_uint32(buffer, 2);
	header.offBits = ConvLE_uint32(buffer, 10);

	if (!(buffer[0] == 'B' && buffer[1] == 'M'))
	{
		cerr << "File is not bitmap" << endl;
		exit(EXIT_FAILURE);
	}

	unsigned int infoSize = ConvLE_uint32(buffer, 14);

	switch (infoSize)
	{
	case 12:	// BMPCOREHEADER
		fin.read((char *)buffer, 8);
		header.width = ConvLE_uint16(buffer, 0);
		header.height = ConvLE_uint16(buffer, 2);
		header.bitCount = ConvLE_uint16(buffer, 6);
		break;
	case 40:	// BMPINFOHEADER
	case 108:	// BITMAPV4HEADER
	case 124:	// BITMAPV5HEADER
		fin.read((char *)buffer, 12);
		header.width = ConvLE_uint32(buffer, 0);
		header.height = ConvLE_uint32(buffer, 4);
		header.bitCount = ConvLE_uint16(buffer, 10);
		break;
	default:
		cout << "Unknown info header; " << infoSize << " bytes" << endl;
		exit(EXIT_FAILURE);
	}

	if (header.bitCount != 8)
	{
		cout << "Unsupported format;" << endl;
		exit(EXIT_FAILURE);
	}

	size_t len = header.width * header.height;
	data = new unsigned char[len];
	
	fin.seekg(header.offBits);
	fin.read((char *)data, len);

	fin.close();
}

void BitmapPicker::Pick(int size, int x, int y, unsigned char result[])
{
	for (int i = 0; i < size; i++)
		for (int j = 0; j < size; j++)
			result[i + j * size] = data[(x + i) + (y + j) * header.width];
}

template<typename T>
void bin(T x)
{
	for (int i = 8 * sizeof(x) - 1; i >= 0; i--)
		std::cout << ((x >> i) % 2 == 1);
	std::cout << std::endl;
}
/*
int main(int argc, char *argv[])
{
	if (argc - 1 < 3)
	{
		std::cerr << "<image> <x> <y>" << std::endl;
		exit(EXIT_FAILURE);
	}

	char *path = argv[1];
	BitmapPicker Picker(path);

	const int w = 130;
	unsigned char p[w * w];

	Picker.Pick(w, (size_t)atoi(argv[2]), (size_t)atoi(argv[3]), p);
	
	writeImage("img.bmp", p, w, w);

	return EXIT_SUCCESS;
}*/

#pragma once

#include <iostream>
#include <fstream>
#include <cstdlib>
#include <cstdint>
#include <string>

using namespace std;

#ifdef _MSC_VER
#pragma pack(push, 1) 
#endif
struct BITMAPFILEHEADER
{
	char		bfType[2];
	uint32_t	bfSize;
	uint16_t	bfReserved1,
				bfReserved2;
	uint32_t	bfOffBits;
}
#ifdef __GNUC__
__attribute__ ((packed))
#endif
;
struct BITMAPINFOHEADER
{
	uint32_t	biSize,
				biWidth,
				biHeight;
	uint16_t	biPlanes,
				biBitCount;
	uint32_t	biCompression,
				biSizeImage,
				biXPelsPerMeter,
				biYPelsPerMeter,
				biClrUsed,
				biClrImportant;
}
#ifdef __GNUC__
__attribute__ ((packed))
#endif
;
struct RGBQUAD
{
	uint8_t	rgbBlue;
	uint8_t	rgbGreen;
	uint8_t	rgbRed;
	uint8_t	rgbReserved;
}
#ifdef __GNUC__
__attribute__ ((packed))
#endif
;
#ifdef _MSC_VER
#pragma pack(pop)
#endif


class BitmapWriter
{
public:
	static void WriteImage(string path, unsigned char image[], int width, int height);
};

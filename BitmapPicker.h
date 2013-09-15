#pragma once

#include <iostream>
#include <fstream>
#include <cstdlib>
#include <cstdint>
#include <string>

using namespace std;

#define ConvLE_uint32(BIN, I)	((uint32_t)BIN[I] + ((uint32_t)BIN[I + 1] << 8) + ((uint32_t)BIN[I + 2] << 16) + ((uint32_t)BIN[I + 3] << 24))
#define ConvLE_uint16(BIN, I)	(uint16_t)((uint16_t)BIN[I] + ((uint16_t)BIN[I + 1] << 8))

typedef struct BitmapHeader
{
	uint32_t size;
	uint32_t offBits;
	uint32_t width, height;
	uint16_t bitCount;
} BitmapFileHeader;

class BitmapPicker
{
	string path;
	BitmapHeader header;
	unsigned char *data;

public:
	BitmapPicker(string bmpfile);
	~BitmapPicker();
	int Width();
	int Height();
	void Pick(int size, int x, int y, unsigned char result[]);
private:
	BitmapPicker(BitmapPicker &);
	BitmapPicker &operator =(const BitmapPicker &);
	void LoadImage();
};

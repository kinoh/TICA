#include "BitmapWriter.h"

void BitmapWriter::WriteImage(string path, unsigned char image[], int width, int height)
{
	uint16_t	padding = 0,
				walign = (uint16_t)(width + (width % 4 == 0 ? 0 : 4 - width % 4));
	uint32_t	offBits = sizeof(struct BITMAPFILEHEADER) + sizeof(struct BITMAPINFOHEADER) + 256 * sizeof(struct RGBQUAD) + padding,
				size = offBits + walign * height;
	struct BITMAPFILEHEADER header = { 'B', 'M', size, 0, 0, offBits };
	struct BITMAPINFOHEADER info = { sizeof(struct BITMAPINFOHEADER), (uint16_t)width, (uint16_t)height, 1, 8, 0, 0, 0xB13, 0xB13, 0, 0 };
	struct RGBQUAD table[256];

	for (int i = 0; i < 256; i++)
	{
		table[i].rgbBlue =
		table[i].rgbGreen =
		table[i].rgbRed = (uint8_t)i;
		table[i].rgbReserved = 0;
	}

	std::ofstream fout(path, std::ios::out | std::ios::binary);

	fout.write((char *)&header, sizeof(header));
	fout.write((char *)&info, sizeof(info));
	fout.write((char *)table, 256 * sizeof(struct RGBQUAD));
	for (int i = 0; i < height; i++)
	{
		fout.seekp(offBits + i * walign);
		fout.write((char *)&image[i * width], width);
	}

	fout.close();
}

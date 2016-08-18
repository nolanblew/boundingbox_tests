#pragma once
namespace ProcoreImaging
{
	public ref class FontEnumerator sealed
	{
	public:
		Platform::Array<Platform::String^>^ ListSystemFonts();
	};
}


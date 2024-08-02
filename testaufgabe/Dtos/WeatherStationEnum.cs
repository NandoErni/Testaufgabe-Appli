using System;
using System.Runtime.Serialization;

namespace testaufgabe.Dtos
{
	public enum WeatherStationEnum
	{
		[EnumMember(Value = "tiefenbrunnen")]
		Tiefenbrunnen,
        [EnumMember(Value = "mythenquai")]
        Mythenquai,
    }
}


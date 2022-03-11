﻿namespace Masa.Auth.Service.Domain.Subjects.Aggregates;

public class AddressValue : ValueObject
{
    public string Address { get; set; } = string.Empty;

    public string ProvinceCode { get; set; } = string.Empty;

    public string CityCode { get; set; } = string.Empty;

    public string DistrictCode { get; set; } = string.Empty;

    public AddressValue()
    {

    }

    public AddressValue(string address, string provinceCode, string cityCode, string districtCode)
    {
        Address = address;
        ProvinceCode = provinceCode;
        CityCode = cityCode;
        DistrictCode = districtCode;
    }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Address;
        yield return ProvinceCode;
        yield return CityCode;
        yield return DistrictCode;
    }
}

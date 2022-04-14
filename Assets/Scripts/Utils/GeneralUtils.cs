using System.Collections.Generic;
using UnityEngine;

public class GeneralUtils
{

    public List<string> GetAllPossibleRefreshRatesString()
    {
        List<string> refreshRates = new List<string>();
        Resolution[] possibleResolutions = Screen.resolutions;

        foreach (Resolution resolution in possibleResolutions)
        {
            if (refreshRates.Count == 0)
                refreshRates.Add("30");

            if (resolution.refreshRate % 5 == 0)
            {
                if (!refreshRates.Exists(rate => rate == resolution.refreshRate.ToString()))
                {
                    refreshRates.Add(resolution.refreshRate.ToString());
                }
            }
        }

        refreshRates.Sort();

        return refreshRates;
    }

    public List<int> GetAllPossibleRefreshRates()
    {
        List<int> refreshRates = new List<int>();
        Resolution[] possibleResolutions = Screen.resolutions;
        foreach (Resolution resolution in possibleResolutions)
        {
            if (refreshRates.Count == 0)
                refreshRates.Add(30);

            if (resolution.refreshRate % 5 == 0)
            {
                if (!refreshRates.Exists(rate => rate == resolution.refreshRate))
                {
                    refreshRates.Add(resolution.refreshRate);
                }
            }
        }
        refreshRates.Sort();
        return refreshRates;
    }

    public List<CustomResolution> GetAllPossibleResolutions()
    {
        List<CustomResolution> resolutions = new List<CustomResolution>();
        Resolution[] possibleResolutions = Screen.resolutions;
        foreach (Resolution resolution in possibleResolutions)
        {
            if (resolutions.Count > 0)
            {
                if (!resolutions.Exists(res => res.displayRes == resolution.width.ToString() + " x " + resolution.height.ToString()) && resolution.height <= Display.main.systemHeight)
                    resolutions.Add(new CustomResolution(resolution.width, resolution.height));
            }
            else
                resolutions.Add(new CustomResolution(resolution.width, resolution.height));
        }
        return resolutions;
    }

    public List<string> GetAllPossibleResolutionsString()
    {
        List<string> resolutions = new List<string>();
        Resolution[] possibleResolutions = Screen.resolutions;
        foreach (Resolution resolution in possibleResolutions)
        {
            if (resolutions.Count > 0)
            {
                if (!resolutions.Exists(res => res == resolution.width.ToString() + " x " + resolution.height.ToString()) && resolution.height <= Display.main.systemHeight)
                    resolutions.Add(resolution.width.ToString() + " x " + resolution.height.ToString());
            }
            else
                resolutions.Add(resolution.width.ToString() + " x " + resolution.height.ToString());
        }
        return resolutions;
    }
}

public class CustomResolution
{
    public int width;
    public int height;
    public string displayRes;

    public CustomResolution(int width, int height)
    {
        this.width = width;
        this.height = height;
        displayRes = width.ToString() + " x " + height.ToString();
    }
}

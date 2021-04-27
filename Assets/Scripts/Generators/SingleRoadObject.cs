using Services.Generators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DensityViewable
{
    public float density;
    public ViewableObject template;
}

[System.Serializable]
public struct DensityCurve
{
    public AnimationCurve curve;
    public int stepCount;
}

[CreateAssetMenu(fileName = "SingleRoad", menuName = "Generators/ProceduralGenerators/SingleRoad")]
public class SingleRoadObject : GeneratorObject
{
    public uint lineCount;
    public float lineThickness;
    public int lineLength;
    public float startSpeed;
    public float startDistance;

    public int seed;
    public DensityCurve roadDensity;
    public DensityCurve spaceDensity;
    public DensityViewable[] roadTemplates;
    public DensityViewable[] spaceTemplates;

    protected override ILevelGeneratorFactory ChooseGeneratorFactory(IList<ILevelGeneratorFactory> generatorFactories)
    {
        foreach(ILevelGeneratorFactory generatorFactory in generatorFactories)
        {
            SingleRoadGeneratorFactory singleRoadFactory = (SingleRoadGeneratorFactory)generatorFactory;
            if (singleRoadFactory != null)
            {
                SetupGeneratorFactory(singleRoadFactory);
                return singleRoadFactory;
            }
        }
        return null;
    }

    private void SetupGeneratorFactory(SingleRoadGeneratorFactory singleRoadFactory)
    {
        LevelParams levelSettings;
        levelSettings.lineCount = lineCount;
        levelSettings.lineThickness = lineThickness;
        levelSettings.lineLength = lineLength;
        levelSettings.startSpeed = startSpeed;
        levelSettings.startDistance = startDistance;

        SingleRoadParams singleRoadSettings;
        singleRoadSettings.seed = seed;

        float[] roadDensityf = new float[roadDensity.stepCount];
        for (int i = 0; i < roadDensity.stepCount; i++){
            roadDensityf[i] = roadDensity.curve.Evaluate(i / (float)(roadDensity.stepCount-1));
        }
        singleRoadSettings.roadDensity = roadDensityf;

        float[] spaceDensityf = new float[spaceDensity.stepCount];
        for (int i = 0; i < spaceDensity.stepCount; i++)
        {
            spaceDensityf[i] = spaceDensity.curve.Evaluate(i / (float)(spaceDensity.stepCount-1));
        }
        singleRoadSettings.spaceDensity = spaceDensityf;

        singleRoadSettings.roadTemplates = new DensityTemplate[roadTemplates.Length];
        for(int i = 0; i < roadTemplates.Length; i++)
        {
            singleRoadSettings.roadTemplates[i].density = roadTemplates[i].density;
            singleRoadSettings.roadTemplates[i].template = roadTemplates[i].template.GetViewableResource();
        }

        singleRoadSettings.spaceTemplates = new DensityTemplate[spaceTemplates.Length];
        for (int i = 0; i < spaceTemplates.Length; i++)
        {
            singleRoadSettings.spaceTemplates[i].density = spaceTemplates[i].density;
            singleRoadSettings.spaceTemplates[i].template = spaceTemplates[i].template.GetViewableResource();
        }

        singleRoadFactory.SetupSingleRoad(levelSettings, singleRoadSettings);
    }
}

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

[CreateAssetMenu(fileName = "SingleRoad", menuName = "Generators/ProceduralGenerators/SingleRoad")]
public class SingleRoadObject : GeneratorObject
{
    public uint lineCount;
    public float lineThickness;
    public int lineLength;
    public float startSpeed;
    public float startDistance;

    public int seed;
    public float roadProbability;
    public float spaceProbability;
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
        LevelSettings levelSettings;
        levelSettings.lineCount = lineCount;
        levelSettings.lineThickness = lineThickness;
        levelSettings.lineLength = lineLength;
        levelSettings.startSpeed = startSpeed;
        levelSettings.startDistance = startDistance;

        SingleRoadSettings singleRoadSettings;
        singleRoadSettings.seed = seed;
        singleRoadSettings.roadProbability = roadProbability;
        singleRoadSettings.spaceProbability = spaceProbability;

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

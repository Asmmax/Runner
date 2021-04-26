namespace Services.Localization
{
    [System.Serializable]
    public enum Local { RU, ENG};
    public interface ITextLocalizationService
    {
        void SetLocal(Local local);
        string TranslateText(string id);
    }
}

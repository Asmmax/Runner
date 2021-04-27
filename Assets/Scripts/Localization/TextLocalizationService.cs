
public class TextLocalizationService : ITextLocalizationService
{
    private LocalizationSettings settings;

    private Local targetLocal;

    public TextLocalizationService(LocalizationSettings settings)
    {
        this.settings = settings;
        targetLocal = settings.defaultLocal;
    }

    public void SetLocal(Local local)
    {
        targetLocal = local;
    }

    public string TranslateText(string id)
    {
        foreach (var table in settings.tables)
        {
            string translated = table.TranslateText(id, targetLocal);
            if (translated != null)
                return translated;
        }
        return null;
    }
}

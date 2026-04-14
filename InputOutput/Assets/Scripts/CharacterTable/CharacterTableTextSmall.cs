public class CharacterTableTextSmall : CharacterTableText
{
    public CharacterTableTextBig bigText;

    public void OnClick()
    {
        bigText.SetData(id);
    }
}
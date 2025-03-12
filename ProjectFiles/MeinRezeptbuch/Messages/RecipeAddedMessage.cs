using CommunityToolkit.Mvvm.Messaging.Messages;

public class RecipeAddedMessage : ValueChangedMessage<bool>
{
    public RecipeAddedMessage() : base(true) { }
}

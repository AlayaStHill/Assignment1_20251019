namespace Infrastructure.Interfaces
{
    public interface IUIService
    {
        void AddSpacing();
        int GetNumberInput(string message, int min = 1, int max = int.MaxValue);
        void NewPage(string message);
        void PrintErrorMessage(string errorMessage);
        void PrintMessage(string message);
        void ShowList(List<string> options);
        string UserInput(string message);
        string? UserInputNullable(string message);
        void WaitForUserRespons();
    }
}
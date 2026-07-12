using WorkoutTracker.ViewModels;

namespace WorkoutTracker.Views;

public partial class ExercisesListPage : ContentPage
{
	private readonly ExerciseViewModel _exerciseViewModel;

	public ExercisesListPage(ExerciseViewModel exerciseViewModel)
	{
		InitializeComponent();
		BindingContext = _exerciseViewModel = exerciseViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _exerciseViewModel.LoadExercisesAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}
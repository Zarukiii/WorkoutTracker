using WorkoutTracker.ViewModels;

namespace WorkoutTracker.Views;

public partial class MusclesListPage : ContentPage
{
	private readonly MuscleViewModel _muscleViewModel;

	public MusclesListPage(MuscleViewModel muscleViewModel)
	{
		InitializeComponent();
		BindingContext = _muscleViewModel = muscleViewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _muscleViewModel.LoadMusclesAsync();
	}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}
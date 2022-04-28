using System.Windows;

namespace FileExplorer
{
	public enum InputType
	{
		Rename,
		Move
	}
	public partial class InputBox : Window
	{
		private InputType CurrentInputType { get; set; }

		public InputBox()
		{
			InitializeComponent();
		}

		public InputBox(InputType inputType)
		{
			InitializeComponent();

			CurrentInputType = inputType;

			switch (CurrentInputType)
			{
				case InputType.Rename:
					Title = "Rename";
					Headline_TextBlock.Text = "Please choose a new name";
					break;
				case InputType.Move:
					Title = "Move";
					Headline_TextBlock.Text = "Please choose a new destination";
					break;
				default:
					break;
			}
		}

		private void Ok_Button_Click(object sender, RoutedEventArgs e)
		{
			string input = Input_TextBox.Text.Trim();
			string backslash = "\\";

			switch (CurrentInputType)
			{
				case InputType.Rename:
					MainWindow.NewName = input;
					break;
				case InputType.Move:
					if (!input[input.Length - 1].Equals(backslash)) { input += backslash; }
					MainWindow.NewDestination = input;
					break;
				default:
					break;
			}

			this.Close();
		}
	}
}

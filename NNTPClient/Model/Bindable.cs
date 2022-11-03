using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NNTPClient.Model
{
	public abstract class Bindable : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void propertyIsChanged([CallerMemberName] string memberName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}
}

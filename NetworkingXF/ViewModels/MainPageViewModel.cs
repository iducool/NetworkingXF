using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using NetworkingXF.WebManager;

namespace NetworkingXF.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        readonly IUserService _userService;
        public MainPageViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await _userService.GetPostsAsync();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}

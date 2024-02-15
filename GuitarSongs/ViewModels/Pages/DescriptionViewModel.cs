// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using GuitarSongs.Helpers;
using Wpf.Ui;

namespace GuitarSongs.ViewModels.Pages;

public partial class DescriptionViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public DescriptionViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void OnCardClick(string parameter)
    {
        if (String.IsNullOrWhiteSpace(parameter))
        {
            return;
        }

        Type? pageType = NameToPageTypeConverter.Convert(parameter);

        if (pageType == null)
        {
            return;
        }

        _ = _navigationService.Navigate(pageType);
    }
}

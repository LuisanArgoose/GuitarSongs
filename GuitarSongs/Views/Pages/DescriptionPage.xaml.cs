// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using GuitarSongs.ViewModels.Pages;

namespace GuitarSongs.Views.Pages;

public partial class DescriptionPage : INavigableView<DescriptionViewModel>
{
    public DescriptionViewModel ViewModel { get; }

    public DescriptionPage(DescriptionViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}

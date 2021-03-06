﻿using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using System;
using Template10.Services.Serialization;
using Tenplex.Models;
using Tenplex.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Tenplex.Views
{
    public sealed partial class AlbumsPage : Page
    {
        private readonly ISerializationService _serializationService;
        private AlbumsPageViewModel ViewModel => (DataContext as AlbumsPageViewModel);

        public AlbumsPage()
        {
            InitializeComponent();
            _serializationService = Prism.PrismApplicationBase.Current.Container.Resolve<ISerializationService>();
        }

        private async void AlbumsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Prepare connected artwork animation.
            AlbumsGridView.PrepareConnectedAnimation("albumImage", e.ClickedItem, "AlbumArtworkImage");

            var path = PathBuilder.Create(nameof(AlbumPage), ("ratingKey", (e.ClickedItem as Album).RatingKey)).ToString();
            var container = Prism.PrismApplicationBase.Current.Container as UnityContainerExtension;
            await container.Resolve<ShellPage>().ShellView.NavigationService.NavigateAsync(path, null, new DrillInNavigationTransitionInfo());
        }

        private async void ArtistsButton_Click(object sender, RoutedEventArgs e)
        {
            PageRegistry.RemoveRegistration("MusicPage");
            var container = Prism.PrismApplicationBase.Current.Container as UnityContainerExtension;

            container.RegisterForNavigation<ArtistsPage, ArtistsPageViewModel>("MusicPage");
            await container.Resolve<ShellPage>().ShellView.NavigationService.NavigateAsync(PathBuilder.Create("MusicPage", ("sectionKey", ViewModel.SectionKey)).ToString());
        }
    }
}

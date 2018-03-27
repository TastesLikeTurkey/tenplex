﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Services.Web;
using Template10.Utilities;
using Tenplex.Models;

namespace Tenplex.Services
{
    public sealed class AlbumsService
    {
        private readonly ConnectionsService _connectionsService;
        private readonly IWebApiService _webApiService;

        public ObservableCollection<Album> Albums { get; } = new ObservableCollection<Album>();

        public AlbumsService(ConnectionsService connectionsService, IWebApiService webApiService)
        {
            _connectionsService = connectionsService ?? throw new ArgumentNullException(nameof(connectionsService));
            _webApiService = webApiService ?? throw new ArgumentNullException(nameof(webApiService));
        }

        public async Task LoadAlbumsAsync(string sectionKey)
        {
            // this._webApiService.AddHeader("Accept", "application/json");

            var url = $"{_connectionsService.CurrentConnection.Uri}/library/sections/{sectionKey}/albums";
            var result = await _webApiService.GetAsync(new Uri(url));

            var jObj = JObject.Parse(result);

            try
            {
                var directory = jObj.SelectToken("MediaContainer.Metadata");
                var albums = directory.ToObject<IEnumerable<Album>>();
                Albums.AddRange(albums, true);
            }

            catch
            {
                // This can happen if there are no albums; the Metadata token won't exist.
                Albums.Clear();
            }
        }
    }
}

for (var albumIndex = 0; albumIndex < albums.length; albumIndex++) {
            this._albumsData[albumIndex] = {
                title: albums[albumIndex].title,
                artist: albums[albumIndex].artist,
                artwork: albums[albumIndex].artwork,
                tracks: albums[albumIndex].tracks
            };
        }

function getSelectedAlbum() {
    var albumArtist = getSelectedAlbumInfo();
    if (albumArtist === null) return null;
    for (var i = 0; i < albums.length; i++) {
        var album = albums[i];
        if (albumArtist.album !== album.title || albumArtist.artist !== album.artist) continue;
        return album;
    }
}

function getSelectedTrack() {
    var tracksTable = document.getElementById("tracksTable");
    for (var i = 0; i < tracksTable.rows.length; i++) {
        var button = tracksTable.rows[i].cells[0].children[0];
        if (button.getAttribute("data-selected") === "true") {
            return button.value;
        }
    }
}

function deselectAllButtons(table) {
    for (var i = 0; i < table.rows.length; i++) {
        var button = table.rows[i].cells[0].children[0];
        button.setAttribute("data-selected", "false");
        button.style.backgroundColor = buttonColours.offNonSelected;
    }

}

function songButtonToAlbum(songButton) {
    for (var i = 0; i < albums.length; i++) {
        if (songButton.getAttribute("data-artist") === albums[i].artist &&
            songButton.getAttribute("data-album") === albums[i].title) {
            return albums[i];
        }
    }
    return null;
}

function albumButtonToAlbum(albumButton) {
    for (var i = 0; i < albums.length; i++) {
        if (albumButton.getAttribute("data-artist") === albums[i].artist &&
            albumButton.getAttribute("data-album") === albums[i].title) {
            return albums[i];
        }
    }
    return null;
}
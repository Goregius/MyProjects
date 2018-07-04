var selectedAlbum; //keeps track of the currently selected album.
var selectedTrackName; //keeps track of the currently selected track name.
var playingTrackIndexes = {albumIndex: -1, trackIndex: -1}; //keeps track of the indexes required to know the current track stored in music.js.

window.onload = function () {
    initializeKeyboard();
    updateMainContent();
};


function initializeKeyboard() {
    //Stores the colours of the keyboard buttons for easier readability.
    const keyColours = {
        hoverBackground: '#E5E5E5', offBackground: '#333333', downBackground: '#038487',
        hoverForeground: 'black', offForeground: 'white', downForeground: 'white'
    };

    var searchBar = document.getElementById("searchBar");
    setAllKeys();
    setAlphaKeys(searchBar); //A..Z and space keys.
    setBackSpace(searchBar);
    setClearKey(searchBar);

    //Sets the colours of the keys for different mouse events for each keyboard button.
    function setAllKeys() {
        var keys = document.querySelectorAll(".keyboardButton, .clear, .space, .backspace");
        for (var i = 0; i < keys.length; i++) {
            //Makes sure that the key colours are set correctly to start (in case the css is incorrect).
            keys[i].style.color = keyColours.offForeground;
            keys[i].style.backgroundColor = keyColours.offBackground;

            //These set the appropriate colours for different mouse events.
            keys[i].onmouseup = function () {
                this.style.color = keyColours.hoverForeground;
                this.style.backgroundColor = keyColours.hoverBackground;
            };
            keys[i].onmousedown = function () {
                this.style.color = keyColours.downForeground;
                this.style.backgroundColor = keyColours.downBackground;
            };
            keys[i].onmouseenter = function () {
                this.style.color = keyColours.hoverForeground;
                this.style.backgroundColor = keyColours.hoverBackground;

            };
            keys[i].onmouseleave = function () {
                this.style.color = keyColours.offForeground;
                this.style.backgroundColor = keyColours.offBackground;
            };
        }
    }

    function setAlphaKeys(searchBar) {
        var alphaKeys = document.querySelectorAll(".keyboardButton, .space");
        //Iterates through every alpha key.
        for (var i = 0; i < alphaKeys.length; i++) {
            alphaKeys[i].onclick = function () {
                //concatenates the value of the button to the search bar.
                searchBar.value += this.value;
                //Doesn't run the search checking if the search bar value has a length of 1.
                if (searchBar.value.length > 1) {
                    onTextChange()
                }
            };
        }
    }

    function setBackSpace(searchBar) {
        var backSpace = document.getElementById("backspace");
        backSpace.onclick = function () {
            //Removes the last character in the search bar.
            searchBar.value = searchBar.value.slice(0, -1);
            onTextChange()
        };
    }

    function setClearKey(searchBar) {
        var clearKey = document.getElementById("clear");
        clearKey.onclick = function () {
            //Clears the search bar text.
            searchBar.value = "";
            onTextChange();
        };
    }

    function onTextChange() {
        updateMainContent();
    }
}

//Updates the main content (everything in the contentContainer).
function updateMainContent() {
    //Stores the colours of the buttons for easier readability.
    const buttonColours = {onButton: "#282829", offSelected: "#333333", offNonSelected: "transparent"};
    //Firstly updates what buttons to add to the albums table.
    updateAlbumsSection();
    var albumIndex = albums.indexOf(selectedAlbum);
    //If an album button hasn't been clicked then don't update the tracks list.
    if (albumIndex !== -1) {
        updateTracksSection(albumIndex);
    }

    function updateAlbumsSection() {
        var table = document.getElementById("albumTable");
        table.innerHTML = "";
        //Iterates through every album in albums from music.js.
        for (var i = 0; i < albums.length; i++) {
            //If the album matches the search bar's value then create the button
            //otherwise don't create the button
            if (!albumMatchesSearchBar(albums[i])) continue;

            //These create the row, row cell and the button within the cell to be put into the html at the end of the function.
            var tr = document.createElement("tr");
            var td = document.createElement("td");
            var button = document.createElement("input");

            //Sets the default properties of the album and track buttons.
            setDefaultButtonProperties(button);

            button.value = albums[i].artist + " - " + albums[i].title;
            //Sets the album index to i, which is stored in the tag of the button
            button.setAttribute("data-albumIndex", i.toString());

            //Sets the button to the off selected colour if it's the selected album
            if (selectedAlbum === albums[i]) {
                button.style.backgroundColor = buttonColours.offSelected;
                button.setAttribute("data-selected", "true");
            }

            button.onclick = function() {
                //Gets the album index from the button's data-albumIndex attribute.
                var albumIndex = parseInt(this.getAttribute("data-albumIndex"));

                if (selectedAlbum === albums[albumIndex]) return;

                selectButton(table, this);
                selectedAlbum = albums[albumIndex];
                this.setAttribute("data-selected", "true");
                updateTracksSection(albumIndex);
            };

            button.ondblclick = function() {
                var albumIndex = parseInt(this.getAttribute("data-albumIndex"));
                //Selects the first track in the album.
                var trackIndex = selectTrackButton(0);
                //Set's the track indexes.
                setPlayingIndexes(parseInt(albumIndex), parseInt(trackIndex));
                //Plays the track..
                updateAudioPlayer();
            };
            //Adds the table to the html.
            td.appendChild(button);
            tr.appendChild(td);
            table.appendChild(tr);
        }

        function albumMatchesSearchBar(album) {
            var searchValue = document.getElementById("searchBar").value;
            //Matches if the search value length is at most 1.
            if (searchValue.length <= 1) return true;

            //Iterates all the tracks in the album and returns true if one of them has a matching title or matching lyrics.
            for (var i = 0; i < album.tracks.length; i++) {
                var lyrics = album.tracks[i].lyrics;
                var title = album.tracks[i].title;
                if (lyrics.toUpperCase().indexOf(searchValue.toUpperCase()) >= 0 ||
                    title.toUpperCase().indexOf(searchValue.toUpperCase()) >= 0) {
                    return true
                }
            }
            return false;
        }

        function selectTrackButton(trackNumber) {
            var table = document.getElementById("tracksTable");
            var button = table.rows[trackNumber].cells[0].childNodes[0];
            selectedTrackName = button.getAttribute("data-track");
            return parseInt(button.getAttribute("data-trackIndex"));
        }
    }
    //Works similarly to updateAlbumsSection.
    function updateTracksSection(albumIndex) {
        var album = selectedAlbum;
        if (album === null) return;
        var table = document.getElementById("tracksTable");
        makeDetailHeadersVisible(album);
        table.innerHTML = "";
        var trackAdded = false;
        for (var i = 0; i < album.tracks.length; i++) {
            //Adds the track to the list only if it matches with the search bar.
            if (!trackMatchesSearchBar(album.tracks[i].lyrics, album.tracks[i].title)) continue;

            var tr = document.createElement("tr");
            var td = document.createElement("td");
            var button = document.createElement("input");

            setDefaultButtonProperties(button);

            button.value = album.tracks[i].title;

            //Sets the album and track index in the tag of the button.
            button.setAttribute("data-albumIndex", albumIndex.toString());
            button.setAttribute("data-trackIndex", i.toString());

            if (selectedTrackName === album.tracks[i].title) {
                button.style.backgroundColor = buttonColours.offSelected;
                button.setAttribute("data-selected", "true");
            }

            button.onclick = function () {
                var albumIndex = parseInt(this.getAttribute("data-albumIndex"));
                var trackIndex = parseInt(this.getAttribute("data-trackIndex"));
                selectButton(table, this);
                this.setAttribute("data-selected", "true");
                selectedTrackName = albums[albumIndex].tracks[trackIndex].title;
            };

            button.ondblclick = function () {
                var albumIndex = this.getAttribute("data-albumIndex");
                var trackIndex = this.getAttribute("data-trackIndex");
                setPlayingIndexes(parseInt(albumIndex), parseInt(trackIndex));
                updateAudioPlayer(albumIndex, trackIndex);
            };

            var addedButton = td.appendChild(button);
            tr.appendChild(td);
            table.appendChild(tr);
            trackAdded = true;
        }
        //Hides the tracks header if none of the tracks are found.
        if (!trackAdded) {
            document.getElementById("tracksTable").innerHTML = "";
            hideTracksHeader();

            function hideTracksHeader() {
                var artistHeader = document.getElementById("songContainerArtist");
                var albumHeader = document.getElementById("songContainerAlbum");
                artistHeader.style.visibility = "hidden";
                albumHeader.style.visibility = "hidden";
            }
        }

        function makeDetailHeadersVisible(album) {
            var artistHeader = document.getElementById("songContainerArtist");
            var albumHeader = document.getElementById("songContainerAlbum");
            artistHeader.innerHTML = album.artist;
            artistHeader.style.visibility = "visible";
            albumHeader.innerHTML = album.title;
            albumHeader.style.visibility = "visible";
        }

        function trackMatchesSearchBar(lyrics, trackName) {
            var searchValue = document.getElementById("searchBar").value;
            lyrics = lyrics.toUpperCase();
            trackName = trackName.toUpperCase();
            searchValue = searchValue.toUpperCase();
            if (searchValue.length <= 1) return true;
            return (lyrics.indexOf(searchValue) >= 0 || trackName.indexOf(searchValue) >= 0);
        }
    }

    function setDefaultButtonProperties(button) {
        button.type = "button";
        button.className = "listButton";
        button.setAttribute("data-selected", "false");

        button.onmouseenter = function () {
            this.style.backgroundColor = buttonColours.onButton;
        };
        button.onmouseleave = function () {
            if (this.getAttribute("data-selected") === "true") {
                this.style.backgroundColor = buttonColours.offSelected;
            }
            else {
                this.style.backgroundColor = buttonColours.offNonSelected;
            }
        };

    }

    function setPlayingIndexes(albumIndex, trackIndex) {
        playingTrackIndexes.albumIndex = parseInt(albumIndex);
        playingTrackIndexes.trackIndex = parseInt(trackIndex);
    }

    function selectButton(table, mainButton) {
        for (var i = 0; i < table.rows.length; i++) {
            var button = table.rows[i].cells[0].children[0];
            if (button !== mainButton) {
                button.setAttribute("data-selected", "false");
                button.style.backgroundColor = buttonColours.offNonSelected;
            }
        }
    }
}

//Updates and plays the track from playingTrackIndexes.
function updateAudioPlayer() {
    var divs = document.querySelectorAll(".detailsContainerTop, .detailsContainerBottom");
    for (var i = 0; i < divs.length; i++) {
        divs[i].style.visibility = "visible";
    }
    var albumIndex = playingTrackIndexes.albumIndex;
    var trackIndex = playingTrackIndexes.trackIndex;
    setPlayingTrackDetails(albumIndex, trackIndex);
    setArtwork(albumIndex);
    setAudioSource(albumIndex, trackIndex);

    play();

    //Sets the headers.
    function setPlayingTrackDetails(albumIndex, trackIndex) {
        var albumHeader = document.getElementById("detailsContainerAlbum");
        var artistHeader = document.getElementById("detailsContainerArtist");
        var trackHeader = document.getElementById("detailsContainerTrack");
        var album = albums[albumIndex];
        var track = album.tracks[trackIndex];
        albumHeader.innerHTML = album.title;
        artistHeader.innerHTML = album.artist;
        trackHeader.innerHTML = track.title;
    }
    //Sets the artwork.
    function setArtwork(albumIndex) {
        var artwork = document.getElementById("artworkImg");
        var artworkName = albums[albumIndex].artwork;
        artwork.src = "artwork/" + artworkName + ".jpg";
    }
    function setAudioSource(albumIndex, trackIndex) {
        var album = albums[albumIndex];
        var audioSource = document.getElementById("audioSource");
        audioSource.src =
            "audio/" + album.artist + "/" + album.title + "/"
            + album.tracks[trackIndex].mp3;
        audioSource.type = "audio/mp3";
    }

    function play() {
        var audioPlayer = document.getElementById("musicControl");
        audioPlayer.load();
        audioPlayer.play();
    }
}

function onTrackEnded() {
    playNextTrack();

    function playNextTrack() {
        var length = albums[playingTrackIndexes.albumIndex].tracks.length;
        //Sets the track index to the one above..
        if (playingTrackIndexes.trackIndex === length - 1) {
            playingTrackIndexes.trackIndex = 0;
        }
        else {
            playingTrackIndexes.trackIndex++;
        }
        //plays the next track.
        updateAudioPlayer();
    }
}








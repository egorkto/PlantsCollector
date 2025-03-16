mergeInto(LibraryManager.library, {
	ShowFullscreenAd: function() {
		ShowFullscreen();
	},
	
	LoadExtern: function() {
		LoadData();
	},

	SaveExtern: function(data) {
	    var str = UTF8ToString(data);
        var json = JSON.parse(str);
	    SaveData(json);
	},

	CheckString: function(str) {
		Check(UTF8ToString(str));
	}
})
mergeInto(LibraryManager.library, {

  BindEquipment: function (equipmentid) {
    BindEquipment(equipmentid);
  },
  
  OpenCamera: function (equipmentid) {
    OpenCamera(equipmentid);
  },

   InitSceneFinish: function () {
    Init3DData();
  },

  SaveSwitchData: function (type, sceneid) {
    SaveSwitchData(Pointer_stringify(type),Pointer_stringify(sceneid));
  },


  StringReturnValueFunction: function () {
    var returnStr = "bla";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

   ExitFullScreen: function () {
    ExitFullScreen();
  },
  
   GetUrl: function () {
		var returnStr = location.search; 
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer;  

  },

});
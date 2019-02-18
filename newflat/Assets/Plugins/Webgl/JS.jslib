mergeInto(LibraryManager.library, {

  BindEquipment: function (equipmentid) {
    BindEquipment(equipmentid);
  },
  
  
  OpenCamera: function (equipmentid) {
    OpenCamera(equipmentid);
  },

  SaveSwitchData: function (type, sceneid) {
    SaveSwitchData(Pointer_stringify(type),Pointer_stringify(sceneid));
  },

  PrintFloatArray: function (array, size) {
    for(var i = 0; i < size; i++)
    console.log(HEAPF32[(array >> 2) + i]);
  },

  AddNumbers: function (x, y) {
    return x + y;
  },

  StringReturnValueFunction: function () {
    var returnStr = "bla";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },

});
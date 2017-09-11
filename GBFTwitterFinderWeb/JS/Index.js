"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var FinderView = (function () {
    function FinderView(name, value, level) {
        this.Name = name;
        this.Value = value;
        this.Level = level;
    }
    return FinderView;
}());
exports.FinderView = FinderView;
$(function () {
    var hub = $.connection.twitterHub;
    hub.client.pushNewTwitterMessage = function (message) {
        $('.mdl-layout__content').append(message);
    };
    $.connection.hub.start();
});

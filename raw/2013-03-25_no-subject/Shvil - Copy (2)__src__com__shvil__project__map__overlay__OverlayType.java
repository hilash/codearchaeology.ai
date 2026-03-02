package com.shvil.project.map.overlay;

public enum OverlayType {
    SHVIL(0),
    LISTENING(1),
    ERROR(2),
    LOCATION(3),
    ANGEL(4),
    INFORMATION(5),
    DIARY(6),
    PEOPLE(7),
    WATER(8),
    WATER_HIDE(9),
    SLEEP(10);
 
    private int code;
 
    private OverlayType(int code) {
        this.code = code;
    }
 
    public int getCode() {
        return code;
    }
}
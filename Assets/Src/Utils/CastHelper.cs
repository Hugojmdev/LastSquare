using System;
using System.Collections.Generic;
using UnityEngine;

public static class CastHelper {

    public static bool IsWithin2DBox(Vector2 position, Vector2 castRange, Vector2 direction, List<string> colliders){
        RaycastHit2D hit = Physics2D.BoxCast(position, castRange, 0.0f, direction,  0.0f);
        //Will check if is hitting any collider in the given list of colliders.
        if (hit.collider != null && colliders.Contains(hit.collider.tag)) {
            return true;
        }
        return false;
    }
}
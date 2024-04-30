using System.Collections.Generic;
using UnityEngine;

public static class CastHelper {

    public static bool IsWithin2DBox(Vector2 position, Vector2 castRange, Vector2 direction, List<string> colliders) {
        RaycastHit2D hit = Physics2D.BoxCast(position, castRange, 0.0f, direction,  0.0f);
        //Will check if is hitting any collider in the given list of colliders.
        return hit.collider != null && colliders.Contains(hit.collider.tag);
    }

    public static bool IsWithin2DBox(Vector2 position, Vector2 castRange, Vector2 direction, LayerMask layer) {
        RaycastHit2D hit = Physics2D.BoxCast(position, castRange, 0.0f, direction,  0.0f, layer);
        //Will check if is hitting any collider in the given list of colliders.
        return hit.collider != null;
    }
}
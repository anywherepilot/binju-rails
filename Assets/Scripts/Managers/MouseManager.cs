namespace DigitalRails.Managers
{
    using UnityEngine;

    public class MouseManager : Manager<MouseManager>
    {
        private new Camera camera;

        private void Start()
        {
            this.camera = Camera.main;
        }
        
        private void Update()
        {
            if(!Input.GetMouseButtonDown(0)) return;

            Vector3 worldMousePosition = this.camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldMousePosition.x, worldMousePosition.y), Vector2.zero);
            if(hit.collider == null) return;
            print("Clicked something!");
        }
    }
}

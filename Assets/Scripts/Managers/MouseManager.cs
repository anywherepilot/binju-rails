namespace DigitalRails.Managers
{
    using Map;
    using UnityEngine;
    using Utils;

    public sealed class MouseManager : Manager<MouseManager>
    {
        public Events.MouseDraggedOverMilepost OnMouseDraggedOverMilepost;
        public Events.MainMouseButtonUp OnMainMouseButtonUp;

        private new Camera camera;

        // Caching
        private Collider2D colliderUnderMouse;

        private void Start()
        {
            this.camera = Camera.main;
        }

        private void Update()
        {
            if(Input.GetMouseButtonUp(0))
            {
                this.OnMainMouseButtonUp.Invoke();
            }

            if(Input.GetMouseButtonDown(0))
            {
                Vector2 mouseWorldPosition = GetMouseWorldPosition();
                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);
                if(hit.collider == null) return;
            }
            else if(Input.GetMouseButton(0))
            {
                Vector2 mouseWorldPosition = GetMouseWorldPosition();
                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

                if(hit.collider != this.colliderUnderMouse)
                {
                    if (hit.collider == null) return;
                    this.colliderUnderMouse = hit.collider;
                    Milepost milepostUnderMouse = hit.collider.gameObject.GetComponent<Milepost>();
                    this.OnMouseDraggedOverMilepost.Invoke(milepostUnderMouse);
                }
            }
        }

        private Vector2 GetMouseWorldPosition()
        {
            Vector3 worldMousePosition = this.camera.ScreenToWorldPoint(Input.mousePosition);
            return new Vector2(worldMousePosition.x, worldMousePosition.y);
        }
    }
}

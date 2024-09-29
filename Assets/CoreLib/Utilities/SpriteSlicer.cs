using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CoreLib.Utilities
{
    public static class SpriteSlicer
    {
        public static List<Sprite> SliceSprite(Sprite originalSprite, int columns, int rows)
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            List<Sprite> sprites = new List<Sprite>();

            if (columns <= 0 || rows <= 0)
            {
                Debug.LogError("Columns and rows must be greater than zero.");
                return sprites;
            }

            // Calculate the width and height of each tile.
            int tileWidth = (int)(originalSprite.rect.width / columns);
            int tileHeight = (int)(originalSprite.rect.height / rows);

            // Loop through each row and column in the correct order.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Rect newRect = new Rect(
                        originalSprite.rect.x + j * tileWidth,
                        originalSprite.rect.y + (rows - 1 - i) * tileHeight, // Adjusted to start from the top row
                        tileWidth,
                        tileHeight
                    );

                    Sprite newSprite = Sprite.Create(
                        originalSprite.texture,
                        newRect,
                        new Vector2(0.5f, 0.5f), // Center pivot
                        originalSprite.pixelsPerUnit,
                        0,
                        SpriteMeshType.Tight,
                        originalSprite.border
                    );

                    sprites.Add(newSprite);
                }
            }
            //stopwatch.Stop();
            //Debug.Log($"Sliced {columns * rows} sprites in {stopwatch.ElapsedMilliseconds}ms");
            return sprites;
        }

       
        
        
        public static IEnumerator SliceSpriteAsync(Sprite originalSprite, int columns, int rows, System.Action<List<Sprite>> onCompleted)
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            if (columns <= 0 || rows <= 0)
            {
                Debug.LogError("Columns and rows must be greater than zero.");
                yield break; // Exit if invalid input
            }

            int tileWidth = (int)(originalSprite.rect.width / columns);
            int tileHeight = (int)(originalSprite.rect.height / rows);

            // Buffer to store computed Rects
            NativeArray<Rect> rects = new NativeArray<Rect>(columns * rows, Allocator.TempJob);

            // Schedule the job to compute Rects
            ComputeRectsJob rectsJob = new ComputeRectsJob
            {
                width = tileWidth,
                height = tileHeight,
                startX = originalSprite.rect.x,
                startY = originalSprite.rect.y,
                columns = columns,
                rows = rows,
                rects = rects
            };

            JobHandle handle = rectsJob.Schedule(rects.Length, 64);
            yield return new WaitUntil(() => handle.IsCompleted); // Wait until job is done
            handle.Complete();

            List<Sprite> sprites = new List<Sprite>(rects.Length);

            for (int i = 0; i < rects.Length; i++)
            {
                Sprite newSprite = Sprite.Create(originalSprite.texture, rects[i], new Vector2(0.5f, 0.5f), originalSprite.pixelsPerUnit);
                sprites.Add(newSprite);
                if (columns * rows >= 10 && i % (columns * rows / 10) == 0)
                {
                    yield return null; // Yield every few creations to spread load
                }
            }

            rects.Dispose(); // Clean up native array
            //stopwatch.Stop();
            //Debug.Log($"ASYNC: Sliced {columns * rows} sprites in {stopwatch.ElapsedMilliseconds}ms");
            onCompleted?.Invoke(sprites); // Callback to return the sprites
        }

    struct ComputeRectsJob : IJobParallelFor
    {
        public int width;
        public int height;
        public float startX;
        public float startY;
        public int columns;
        public int rows;
        public NativeArray<Rect> rects;

        public void Execute(int index)
        {
            int column = index % columns;
            int row = index / columns;
            rects[index] = new Rect(
                startX + column * width,
                startY + (rows - 1 - row) * height, // Start from top
                width,
                height
            );
        }
    }
    }

}
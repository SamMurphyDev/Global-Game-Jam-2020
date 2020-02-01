using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int Position;
    public bool North;
    public bool NorthEast;
    public bool East;
    public bool SouthEast;
    public bool South;
    public bool SouthWest;
    public bool West;
    public bool NorthWest;

    public GameObject WallEast;
    public GameObject WallEastNorth;
    public GameObject WallEastWest;
    public GameObject WallEastWestNorth;
    public GameObject WallFloor;
    public GameObject WallNorth;
    public GameObject WallSouth;
    public GameObject WallSouthEast;
    public GameObject WallSouthEastNorth;
    public GameObject WallSouthEastWest;
    public GameObject WallSouthNorth;
    public GameObject WallSouthNorthWest;
    public GameObject WallSouthWest;
    public GameObject WallWest;
    public GameObject WallWestNorth;

    GameObject TileObject;

    public void setDirectionTile(Vector2 dir, bool status) {
        if(dir.x == -1) {
            if(dir.y == -1) {
                SouthEast = status;
            }

            if(dir.y == 0) {
                South = status;
            }

            if(dir.y == 1) {
                SouthWest = status;
            }
        }

        if(dir.x == 0) {
            if(dir.y == -1) {
                East = status;
            }

            if(dir.y == 1) {
                West = status;
            }
        }

        if(dir.x == 1) {
            if(dir.y == -1) {
                NorthEast = status;
            }

            if(dir.y == 0) {
                North = status;
            }

            if(dir.y == 1) {
                NorthWest = status;
            }
        }

        Destroy(TileObject);
        TileSwapper();
    }

    void TileSwapper() {
        if(South && North && East && West) {
            TileObject = Instantiate(WallFloor, transform);
        } else if(!South && North && East && West) {
            TileObject = Instantiate(WallSouth, transform);
        } else if(South && !North && East && West) {
            TileObject = Instantiate(WallNorth, transform);
        } else if(South && North && !East && West) {
            TileObject = Instantiate(WallEast, transform);
        } else if(South && North && East && !West) {
            TileObject = Instantiate(WallWest, transform);
        } else if(!South && !North && East && West) {
            TileObject = Instantiate(WallSouthNorth, transform);
        } else if(!South && North && !East && West) {
            TileObject = Instantiate(WallSouthEast, transform);
        } else if(!South && North && East && !West) {
            TileObject = Instantiate(WallSouthWest, transform);
        } else if(South && !North && !East && West) {
            TileObject = Instantiate(WallEastNorth, transform);
        } else if(South && !North && East && !West) {
            TileObject = Instantiate(WallWestNorth, transform);
        } else if(South && North && !East && !West) {
            TileObject = Instantiate(WallEastWest, transform);
        } else if(South && !North && !East && !West) {
            TileObject = Instantiate(WallEastWestNorth, transform);
        }  else if(!South && North && !East && !West) {
            TileObject = Instantiate(WallSouthEastWest, transform);
        } else if(!South && !North && East && !West) {
            TileObject = Instantiate(WallSouthNorthWest, transform);
        } else if(!South && !North && !East && West) {
            TileObject = Instantiate(WallSouthEastNorth, transform);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

    public BallMovement ballMovement;
    public ScoreController scoreConroller;
    bool theLastHitPlayer1 = true;

    void BounceFromRacket(Collision2D c)
    {
        
        float y = 0 ;
        float y1 = 0;
        float y2 = 0;
        float x = 0 ; 
        Vector3 ballPosition = this.transform.position;
        Vector3 racketPosition = c.gameObject.transform.position;

        float racketHight = c.collider.bounds.size.y;

         
        if (c.gameObject.name == "RacketPlayer1")
        {
            Debug.Log("1 vurdu");
            x = 1;
            y = (ballPosition.y - racketPosition.y) / racketHight;
            y1 = y; 
            theLastHitPlayer1 = true;
            this.ballMovement.MoveBall(new Vector2(x, y));
        }
        else if(c.gameObject.name == "RacketPlayer2")
        {
            Debug.Log("2 vurdu");
            x = -1;
            y = (ballPosition.y - racketPosition.y) / racketHight;
            y2 = y;
            theLastHitPlayer1 = false;
            this.ballMovement.MoveBall(new Vector2(x, y));
        }
        else if (c.gameObject.name == "FasterWall")
        {
            if(theLastHitPlayer1)
            {
                Debug.Log("Faster wall");
                x = 5f;
                y = y1; 
                this.ballMovement.IncreaseHitCounter();
                this.ballMovement.MoveBall(new Vector2(x, y));

            }
            else
            {
                Debug.Log("Faster wall");
                x = -5f;
                y = y2; 
                this.ballMovement.IncreaseHitCounter();
                this.ballMovement.MoveBall(new Vector2(x, y));

            }
           
        }
        else if (c.gameObject.name == "ColderWall")
        {
            
            if (theLastHitPlayer1)
            {
                x = 5f;
                y = y1;
                Debug.Log("colder wall");
                this.ballMovement.DecreaseHitCounter();
                this.ballMovement.MoveBall(new Vector2(x, y));

            }
            else
            {
                x = -5f;
                y = y2;
                Debug.Log("colder wall");
                this.ballMovement.DecreaseHitCounter();
                this.ballMovement.MoveBall(new Vector2(x, y));
            }

        }


        
        this.ballMovement.MoveBall(new Vector2(x, y));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "RacketPlayer1" || collision.gameObject.name == "RacketPlayer2" || collision.gameObject.name == "ColderWall" || collision.gameObject.name == "FasterWall")
        {
            this.BounceFromRacket(collision);
        }
        else if (collision.gameObject.name == "WallLeft") 
        {
            Debug.Log("Collision with WallLeft");
            this.scoreConroller.GoalPlayer2();
            StartCoroutine(this.ballMovement.StartBall(true));
        }
        else if (collision.gameObject.name == "WallRight")
        {
            Debug.Log("Collision with WallRight");
            this.scoreConroller.GoalPlayer1();
            StartCoroutine(this.ballMovement.StartBall(false));
        }
    }
}

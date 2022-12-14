using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments = new List<Transform>();  

    public Transform bodyPrefab;

    public  int InitialSize = 3;

    public static Snake instance;

    public GameOverScreen GameOverScreen;

    readonly int maxPlatform = 0;

    public void GameOver()
    {
        GameOverScreen.Setup(maxPlatform);

    }

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        ResetState();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }

    }

    private void FixedUpdate()
    {

        for(int i =_segments.Count-1; i>0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );


    }


    private void Grow()
    {
        Transform segment = Instantiate(this.bodyPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);

    }

    private void ResetState()
    {
        for (int i = 1; i< _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for(int i=1; i< this.InitialSize; i++)
        {
            _segments.Add(Instantiate(this.bodyPrefab));
        }

        this.transform.position = Vector3.zero;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Food")
        {
            Grow();
            ScoreManager.instance.AddScore();
        } else if (other.tag == "Obstacle")
        {
            GameOver();
            Destroy(gameObject);
            //ResetState();

        }

    }

}

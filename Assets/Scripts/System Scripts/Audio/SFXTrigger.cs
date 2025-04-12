using UnityEngine;

public class SFXTrigger : SFXMaker
{
    [SerializeField] float cooldown = .25f;
    float cooldownTimer = 0f;

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cooldownTimer <= 0)
        {
            base.MakeSound(out float clipLength);
            cooldownTimer = cooldown;
        }
    }
}
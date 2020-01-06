using System.Collections.Generic;
using UnityEngine;
using Assets.Code.EventSytem;

public class MagicSystem : MonoBehaviour
{
    private GameObject spell;

    [SerializeField]
    private List<GameObject> spells;

    [SerializeField]
    private GameObject spellOrigin;

    // Start is called before the first frame update
    void Awake()
    {
        spell = spells[0];
        InputEventSystem.RegisterListener(InputEventSystem.InputState.DOWN, KeyCode.Mouse0, CastSpell);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CastSpell()
    {
        GameObject castSpell = Instantiate(spell, spellOrigin.transform.position, spellOrigin.transform.rotation);
    }
}

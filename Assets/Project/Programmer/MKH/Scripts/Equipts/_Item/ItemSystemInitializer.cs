using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystemInitializer : MonoBehaviour
{
    public static ItemFactory Factory { get; private set; }

    private void Awake()
    {
        Factory = new ItemFactory();

        // 장비 타입별 Creator를 등록
        Factory.Register<EarringData>(new EarringCreator());
        Factory.Register<GlassesData>(new GlassesCreator());
        Factory.Register<GlovesData>(new GlovesCreator());
        Factory.Register<HelmetData>(new HelmetCreator());
        Factory.Register<NecklaceData>(new NecklaceCreator());
        Factory.Register<PantsData>(new PantsCreator());
        Factory.Register<RingData>(new RingCreator());
        Factory.Register<ShirtsData>(new ShirtsCreator());
        Factory.Register<ShoesData>(new ShoesCreator());
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FeedBackOnHit : FeedBack
{
    private PostProcessVolume _profile;
    private AmbientOcclusion _ao;
    private Vignette _vig;

    private void Awake()
    {
        _profile = GetComponent<PostProcessVolume>();
        _profile.profile.TryGetSettings(out _ao);
        _profile.profile.TryGetSettings(out _vig);
    }

    // Update is called once per frame
    void Update()
    {
        if (_vig.smoothness.value > 0.25f)
            _vig.smoothness.value -= Time.deltaTime * 2;

        if (_vig.smoothness.value < 0.25f)
            _vig.smoothness.value = 0.25f;
        //_vig.intensity.value = Mathf.Lerp(1, 0.25f, Time.deltaTime * 2);
    }

    public override void CompleteFeedBack()
    {
        _vig.smoothness.value = 0.25f;
    }

    public override void CreateFeedBack()
    {
        _vig.smoothness.value = 1;
        //_vig.smoothness.value = Mathf.Lerp(1, 0.25f, Time.deltaTime * 2);
    }

}


interface ISozai
{
	/// <summary>
	/// 初期化
	/// </summary>
	void Init();


	/// <summary>
	/// フェード
	/// </summary>
	/// <param name="midiVal"></param>
	void SetAlpha(int midiVal);


	/// <summary>
	/// バスドラのBang
	/// </summary>
	void Bang();
}
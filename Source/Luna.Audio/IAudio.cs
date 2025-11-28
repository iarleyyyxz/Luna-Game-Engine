namespace Luna.Audio
{
    // Public interface for an Audio manager/system.
    // This defines the core functionalities required for sound and music playback.
    public interface IAudio
    {
        /// <summary>
        /// Initializes the audio system and prepares it for use (e.g., setting up sound devices, loading basic resources).
        /// </summary>
        void Init();

        /// <summary>
        /// Plays a sound effect identified by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the sound to play (e.g., "gunshot", "jump").</param>
        /// <param name="vol">The volume level (0.0 to 1.0) for this specific playback. Defaults to 1.0 (full volume).</param>
        /// <param name="loop">If true, the sound will loop continuously until explicitly stopped. Defaults to false.</param>
        void PlaySound(string id, float vol = 1f, bool loop = false);

        /// <summary>
        /// Plays a music track from the specified file path.
        /// Music is typically handled separately from sound effects (e.g., streaming).
        /// </summary>
        /// <param name="path">The file path or resource name of the music track.</param>
        /// <param name="loop">If true, the music will loop continuously. Defaults to true.</param>
        void PlayMusic(string path, bool loop = true);

        /// <summary>
        /// Stops the playback of a specific sound effect or music track, identified by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the sound or music instance to stop.</param>
        void Stop(string id);

        /// <summary>
        /// Sets the overall volume for a specific audio channel (e.g., "SFX", "Music", "UI").
        /// </summary>
        /// <param name="channel">The name of the audio channel to control.</param>
        /// <param name="volume">The desired volume level (0.0 to 1.0) for the channel.</param>
        void SetChannelVolume(string channel, float volume);
    }
}
# DaemonLink
Allows for a daemon to have a separate client command to control it.

The Client simply passes all arguments to the Server in place (excluding the first executable argument) and while the server is processing the arguments, the client receives all the output &amp; send all the input.

This is derived from my Python implementation of this ([which can be found here](https://gist.github.com/Gorea235/7bca172196027cb00cfebd5238d44e9e)), and eventually they will be fully compatible (which would allow for a Python front-end command, with a C# backend daemon, or vise versa if you so desired (although this would be a weird way round due to the complexity in building them)).

The Python version currently works, however once I have written the socket handling in this version, I will replicate it in the Python one, along with some consistency changes (i.e. renaming DaemonServer -> Server & DaemonClient -> Client) to enable working cross-language communication.

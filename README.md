# Q-Bort
A user queue management bot for Discord servers.
<h1>Set-up Commands</h1>

set-ochan
<ul><li>
  Sets the channel ID for a Guild specifying the channel they would like to use the 'open' command exclusively in.
  <ul><li>
  Use command in the channel you want to set.
  </ul></li>
</ul></li>

set-pchan
<ul><li>
  Sets the channel ID for a Guild specifying the channel they would like to use the 'new' command exclusively in.
  <ul><li>
  Use command in the channel you want to set.
  </ul></li>
</ul></li>

set-react
<ul><li>
  Sets the reaction for users to react to.
  <ul><li>
  +set-react :kamiLOVE:
  </ul></li>
</ul></li>

set-role
<ul><li>
  Sets the role the bot will the bot will check that users have.
  <ul><li>
  +set-role @customs
  </ul></li>
</ul></li>

<h1>Queue Commands</h1>

Open
<ul><li>
  Create a new queue for users to join. You must pass a user role @mention. 
  <ul><li>
    +open @customs
  </ul></li>
</ul></li>

Close
<ul><li>
  Close off and clear the queue.
</ul></li>

List
<ul><li>
  Provides a list of all active users in the queue database.
</ul></li>

New
<ul><li>
  Gets and displays [x] number of players who have the lowest number of games played for the next lobbies. If no number is provided the default number will be used. When passing a custom number, it should be the first arguement with the password being after the number.
  <ul><li>
    +new [password]
  </ul></li>
  or
  <ul><li>
    +new [x] [password]
  </ul></li>
</ul></li>

Recall
<ul><li>
  Re-pings the last pulled group with a provided message.
  <ul><li>
    +recall This is an after thought.
  </ul></li>
</ul></li>

Replace
<ul><li>
  Calls a new player to replace one or more that is unable to participate after they have been called. Used by passing @mentions
  <ul><li>
    +replace @Johnny @May
  </ul></li>
</ul></li>

Status
<ul><li>
  Returns the status of the guild's queue.
</ul></li>
<h3>Q-Bort's Future!</h3>
<ul><li>
  Web App for Guild management using Discord OAuth2
</li></ul>

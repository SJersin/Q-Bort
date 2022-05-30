# Q-Bort
A user queue management bot for Discord servers.
<h1>Set-up Commands</h1>

set-ochan
<ul><li>
  Sets the channel ID for a Guild specifying the channel they would like to use the 'open' command exclusively in.
</ul></li>

set-pchan
<ul><li>
  Sets the channel ID for a Guild specifying the channel they would like to use the 'new' command exclusively in.
</ul></li>

set-react
<ul><li>
  Sets the reaction for users to react to.
</ul></li>

set-role
<ul><li>
  Sets the role the bot will the bot will check that users have.
</ul></li>

<h1>Queue Commands</h1>

open
<ul><li>
  Create a new queue for users to join. You must pass a user role @mention. 
  <ul><li>
    Ex: create @customs
  </ul></li>
</ul></li>

close
<ul><li>
  Close off and clear the queue.
</ul></li>

list
<ul><li>
  Provides a list of all active users in the queue database.
</ul></li>

new
<ul><li>
  Gets and displays [x] number of players who have the lowest number of games played for the next lobbies. If no number is provided the default number will be used. When passing a custom number, it should be the first arguement with the password being after the number.
  <ul><li>
    Ex: new [password] or new [x] [password]
  </ul></li>
</ul></li>

recall
<ul><li>
  Re-pings the last pulled group with a provided message.
  <ul><li>
    Ex: recall This is an after thought.
  </ul></li>
</ul></li>

replace
<ul><li>
  Calls a new player to replace one or more that is unable to participate after they have been called. Used by passing @mentions
  <ul><li>
    Ex. replace @Johnny @May
  </ul></li>
</ul></li>

status
<ul><li>
  Returns the status of the guild's queue.
</ul></li>
<h3>Features to be implemented:</h3>
<ul><li>
  Web App for Guild management using Discord OAuth2
</li></ul>

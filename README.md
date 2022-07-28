From a CSV file (.dat), I've created a replay Animation.

Brief explanation of the project.
Reader Script is getting the data file as TextAsset. TextAsset is splitted and replaced into the new Lists by the void ReadCSV(). After making the data meaningful, I've created a Script named GameManager, and created an animation by using those meaningful datas.
Player objects are instantiated. Positioned by the datas, named as Team-Player Number, colored by their teams.
I've used IEnumerators and their essential WaitForSeconds().
Frame counts are subdivided by the requested fps value. So the frames will be shown by one second are calculated and served.
The total frame is 560 </> the requested fps value is 25 = so it should continue till it finishes and it takes around 22 seconds.

You can check first lookout to the project Here.
https://www.youtube.com/watch?v=6uLIbC6pDek&t=2s

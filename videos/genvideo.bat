REM Attention ces commandes marche mieux en ligne de commande
ffmpeg -i "prefab_use.mp4" -vf "fps=18" -start_number 1 "prefab_use-%d.png"
ffmpeg -i "prefab_create.mp4" -vf "fps=18" -start_number 1 "prefab_create-%d.png"
@echo off
setlocal

git clone https://github.com/ronin4net/Plainion.git
git clone https://github.com/ronin4net/Plainion.Windows.git
git clone https://github.com/ronin4net/Plainion.Windows.Editors.git
git clone https://github.com/ronin4net/Plainion.Prism.git
git clone https://github.com/ronin4net/Plainion.AppFw.Wpf.git
git clone https://github.com/ronin4net/Plainion.AppFw.Shell.git

git clone https://github.com/ronin4net/Plainion.CI.git
git clone https://github.com/ronin4net/Plainion.IronDoc.git
git clone https://github.com/ronin4net/Plainion.Flames.git
git clone https://github.com/ronin4net/Plainion.GraphViz.git
git clone https://github.com/ronin4net/Plainion.Forest.git
git clone https://github.com/ronin4net/Plainion.Whiteboard.git
git clone https://github.com/ronin4net/Plainion.Notes.git
git clone https://github.com/ronin4net/Plainion.Bees.git

for /f "delims=" %%i in ('dir /ad /b') do (
  echo --- %%i ---
  cd %%i
  git pull
  cd ..
)

pause

endlocal

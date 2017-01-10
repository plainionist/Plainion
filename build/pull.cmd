@echo off
setlocal

git clone https://github.com/plainionist/Plainion.git
git clone https://github.com/plainionist/Plainion.Windows.git
git clone https://github.com/plainionist/Plainion.Windows.Editors.git
git clone https://github.com/plainionist/Plainion.Prism.git
git clone https://github.com/plainionist/Plainion.AppFw.Wpf.git
git clone https://github.com/plainionist/Plainion.AppFw.Shell.git

git clone https://github.com/plainionist/Plainion.CI.git
git clone https://github.com/plainionist/Plainion.IronDoc.git
git clone https://github.com/plainionist/Plainion.Flames.git
git clone https://github.com/plainionist/Plainion.GraphViz.git
git clone https://github.com/plainionist/Plainion.Forest.git
git clone https://github.com/plainionist/Plainion.Whiteboard.git
git clone https://github.com/plainionist/Plainion.Notes.git
git clone https://github.com/plainionist/Plainion.Bees.git

for /f "delims=" %%i in ('dir /ad /b') do (
  echo --- %%i ---
  cd %%i
  git pull
  cd ..
)

pause

endlocal

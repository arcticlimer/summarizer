# Summarizer

Summarizer is a simple project that receives a markdown file and output its
headers to the standard output.

# Example
```bash
vini@nixos:~/projects/summarizer/Summarizer/src/App$ dotnet run ~/projects/blog/notes/NixOS/hybrid_graphics_nvidia_prime.md
- Table of Contents (line: 6)
- Intro (line: 9)
  - Author's Setup (line: 15)
- NixOS Configuration (line: 25)
- Integrated Intel GPU (line: 45)
- Dedicated NVIDIA GPU (line: 51)
  - Offload Script (line: 77)
    - Examples (line: 97)
  - XServer Configuration (line: 106)
- Integrated Intel GPU (line: 110)
- Dedicated NVIDIA GPU (line: 116)
  - NVIDIA + PRIME Configuration (line: 137)
- Conclusion (line: 160)
- Resources (line: 165)
```

> Note: Currently, comments that start with "#" inside code blocks will be
parsed as headers.

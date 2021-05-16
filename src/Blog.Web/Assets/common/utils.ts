export const Utility = {
  scrollTo: (endPosition: number) => {
    const startTime = +new Date()

    // 运动时间
    const duration = 500
    const endtime = performance.now() + duration
    let id = 0
    // 已经运动的时间
    let time = 0
    function run() {
      // 剩下的距离
      time = endtime - performance.now()
      window.scrollTo(0, endPosition * (1 - Math.pow(time / duration, 2)))
      id = requestAnimationFrame(run)
      if (time <= 0) {
        window.scrollTo(0, endPosition)
        cancelAnimationFrame(id)
      }
    }
    requestAnimationFrame(run)
  }
}

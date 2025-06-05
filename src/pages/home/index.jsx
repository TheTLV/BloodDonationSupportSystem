
import './home.css';
import React from 'react';
import { useState, useRef, useEffect } from 'react';
import { motion, useScroll, useTransform, AnimatePresence } from 'framer-motion';
import { Button, Form, Input, Modal, Divider, notification } from 'antd';
import {
  FaMapMarkerAlt, FaPhone, FaHeartbeat, FaHospital, FaUserMd, FaAmbulance,
  FaProcedures, FaIdCard, FaClock, FaFlask, FaUsers, FaCheckCircle, FaArrowRight,
  FaFacebook, FaTwitter, FaInstagram, FaBars, FaTimes, FaArrowUp, FaRegCalendarAlt,
  FaRegClock, FaWeight, FaTint, FaRegSmile, FaGift, FaStar, FaQuoteLeft,
  FaMoon, FaSun, FaSpinner
} from 'react-icons/fa';
import { useSelector } from 'react-redux';

// Enhanced Feature Card Component
function FeatureCard({ icon, title, description, color = "red", delay = 0 }) {
  const User = useSelector((state)=> state.user)
  return (
  
    <motion.div
      initial={{ opacity: 0, y: 50 }}
      whileInView={{ opacity: 1, y: 0 }}
      viewport={{ once: true }}
      transition={{ duration: 0.6, delay }}
      whileHover={{
        y: -8,
        boxShadow: "0 20px 40px rgba(0,0,0,0.1)",
        scale: 1.02
      }}
      className={`bg-white rounded-2xl p-8 shadow-lg border-t-4 border-${color}-500 hover:border-${color}-600 transition-all duration-300 group`}
    >
      <motion.div
        className={`text-${color}-500 text-4xl mb-6 group-hover:scale-110 transition-transform duration-300`}
        whileHover={{ rotate: 5 }}
      >
        {icon}
      </motion.div>
      <h3 className="text-xl font-bold text-gray-800 mb-3 group-hover:text-gray-900">{title}</h3>
      <p className="text-gray-600 leading-relaxed">{description}</p>
    </motion.div>
  );
}

// Enhanced Process Step Component
function ProcessStep({ number, title, description, icon, isActive = false }) {
  return (
    <motion.div
      initial={{ opacity: 0, x: -50 }}
      whileInView={{ opacity: 1, x: 0 }}
      viewport={{ once: true }}
      transition={{ duration: 0.6, delay: number * 0.1 }}
      className="flex items-start space-x-6 mb-10 group"
    >
      <div className="flex-shrink-0">
        <motion.div
          whileHover={{ scale: 1.1 }}
          className={`flex items-center justify-center w-16 h-16 rounded-full ${
            isActive ? 'bg-red-500 text-white' : 'bg-red-100 text-red-500'
          } font-bold text-xl shadow-lg transition-all duration-300 group-hover:shadow-xl`}
        >
          {number}
        </motion.div>
      </div>
      <div className="flex-1">
        <div className="flex items-center mb-3">
          <motion.div
            className="text-red-500 mr-3 text-xl"
            whileHover={{ scale: 1.2, rotate: 10 }}
          >
            {icon}
          </motion.div>
          <h4 className="text-xl font-semibold text-gray-800 group-hover:text-red-600 transition-colors">
            {title}
          </h4>
        </div>
        <p className="text-gray-600 leading-relaxed">{description}</p>
      </div>
    </motion.div>
  );
}

// Testimonial Component
function TestimonialCard({ name, role, content, rating, delay = 0 }) {
  return (
    <motion.div
      initial={{ opacity: 0, scale: 0.9 }}
      whileInView={{ opacity: 1, scale: 1 }}
      viewport={{ once: true }}
      transition={{ duration: 0.6, delay }}
      className="bg-white rounded-2xl p-8 shadow-lg hover:shadow-xl transition-all duration-300"
    >
      <div className="flex items-center mb-4">
        <FaQuoteLeft className="text-red-500 text-2xl mr-3" />
        <div className="flex text-yellow-400">
          {[...Array(rating)].map((_, i) => (
            <FaStar key={i} className="text-sm" />
          ))}
        </div>
      </div>
      <p className="text-gray-600 mb-6 leading-relaxed italic">"{content}"</p>
      <div className="flex items-center">
        <div className="w-12 h-12 bg-red-100 rounded-full flex items-center justify-center mr-4">
          <FaUserMd className="text-red-500" />
        </div>
        <div>
          <h4 className="font-semibold text-gray-800">{name}</h4>
          <p className="text-gray-500 text-sm">{role}</p>
        </div>
      </div>
    </motion.div>
  );
}

// Loading Skeleton Component
function SkeletonCard() {
  return (
    <div className="bg-white rounded-2xl p-8 shadow-lg animate-pulse">
      <div className="w-12 h-12 bg-gray-200 rounded-full mb-4"></div>
      <div className="h-6 bg-gray-200 rounded mb-3"></div>
      <div className="h-4 bg-gray-200 rounded mb-2"></div>
      <div className="h-4 bg-gray-200 rounded w-3/4"></div>
    </div>
  );
}

function EnhancedHospitalBloodDonation() {

  const contactRef = useRef(null);
  const [loading, setLoading] = useState(false);
  const [open, setOpen] = useState(false);
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [darkMode, setDarkMode] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [activeStep, setActiveStep] = useState(1);

  const { scrollY } = useScroll();
  const y1 = useTransform(scrollY, [0, 300], [0, 100]);
  const y2 = useTransform(scrollY, [0, 300], [0, 50]);
  const showBackToTop = useTransform(scrollY, [300, 600], [0, 1]);

  // Simulate loading
  useEffect(() => {
    const timer = setTimeout(() => setIsLoading(false), 2000);
    return () => clearTimeout(timer);
  }, []);

  // Auto-advance process steps
  useEffect(() => {
    const interval = setInterval(() => {
      setActiveStep(prev => prev >= 5 ? 1 : prev + 1);
    }, 3000);
    return () => clearInterval(interval);
  }, []);

  const scrollToContact = () => {
    contactRef.current?.scrollIntoView({ behavior: 'smooth' });
    setIsMenuOpen(false);
  };

  const scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const showModal = () => {
    setOpen(true);
  };

  const handleOk = () => {
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
      setOpen(false);
      notification.success({
        message: 'Đăng ký thành công!',
        description: 'Chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất.',
        placement: 'topRight',
      });
    }, 2000);
  };

  const handleCancel = () => {
    setOpen(false);
  };

  const toggleDarkMode = () => {
    setDarkMode(!darkMode);
  };

  // Hospital Info
  const hospitalInfo = {
    name: 'Bệnh viện Đa khoa Thành phố Hồ Chí Minh',
    shortName: 'BV ĐK TP.HCM',
    address: '125 Lê Lợi, Phường Bến Nghé, Quận 1, TP.HCM',
    phone: '028 3829 5723',
    hotline: '1900 1234',
    email: 'hienmau@bvdktphcm.vn',
    workingHours: 'Thứ 2 - Thứ 6: 7:30 - 16:30 | Thứ 7: 8:00 - 12:00',
    donationFrequency: 'Hàng ngày (trừ Chủ nhật)',
    capacity: 'Tiếp nhận 50-80 người hiến máu/ngày'
  };

  // Enhanced Features Data
  const features = [
    {
      icon: <FaHospital />,
      title: 'Cơ sở vật chất hiện đại',
      description: 'Trang thiết bị y tế tiên tiến, đảm bảo quy trình hiến máu an toàn và vô trùng.',
      color: 'red'
    },
    {
      icon: <FaUserMd />,
      title: 'Đội ngũ chuyên môn cao',
      description: 'Bác sĩ và điều dưỡng giàu kinh nghiệm, được đào tạo chuyên sâu về hiến máu.',
      color: 'blue'
    },
    {
      icon: <FaAmbulance />,
      title: 'Kết nối với bệnh nhân',
      description: 'Máu hiến tặng phục vụ trực tiếp bệnh nhân tại bệnh viện, cứu sống nhiều người.',
      color: 'green'
    },
    {
      icon: <FaProcedures />,
      title: 'Chăm sóc sau hiến',
      description: 'Theo dõi sức khỏe định kỳ và tư vấn dinh dưỡng miễn phí cho người hiến máu.',
      color: 'purple'
    }
  ];

  // Requirements Data
  const requirements = [
    {
      icon: <FaIdCard />,
      title: 'Giấy tờ tùy thân',
      description: 'CMND/CCCD hoặc hộ chiếu còn hiệu lực'
    },
    {
      icon: <FaWeight />,
      title: 'Cân nặng',
      description: 'Tối thiểu 45kg trở lên'
    },
    {
      icon: <FaRegClock />,
      title: 'Thời gian giữa các lần',
      description: 'Tối thiểu 12 tuần với nam, 16 tuần với nữ'
    },
    {
      icon: <FaTint />,
      title: 'Sức khỏe tốt',
      description: 'Không mắc các bệnh truyền nhiễm'
    }
  ];

  // Enhanced Process Data
  const processSteps = [
    {
      number: '1',
      title: 'Đăng ký thông tin',
      description: 'Điền phiếu đăng ký chi tiết và xuất trình giấy tờ tùy thân hợp lệ',
      icon: <FaIdCard />
    },
    {
      number: '2',
      title: 'Khám sàng lọc',
      description: 'Kiểm tra cân nặng, huyết áp, nhiệt độ và xét nghiệm máu nhanh',
      icon: <FaUserMd />
    },
    {
      number: '3',
      title: 'Tư vấn sức khỏe',
      description: 'Bác sĩ tư vấn chi tiết về quy trình và kiểm tra tình trạng sức khỏe',
      icon: <FaHeartbeat />
    },
    {
      number: '4',
      title: 'Hiến máu',
      description: 'Thực hiện hiến máu trong phòng vô trùng với thiết bị y tế hiện đại',
      icon: <FaFlask />
    },
    {
      number: '5',
      title: 'Nghỉ ngơi & nhận quà',
      description: 'Nghỉ ngơi 15-20 phút, nhận giấy chứng nhận và quà lưu niệm',
      icon: <FaGift />
    }
  ];

  // Enhanced Stats Data
  const stats = [
    { value: '5.200+', label: 'Người hiến máu/năm', icon: <FaUsers />, color: 'from-blue-500 to-blue-600' },
    { value: '15.000+', label: 'Đơn vị máu thu/năm', icon: <FaTint />, color: 'from-red-500 to-red-600' },
    { value: '98%', label: 'Hài lòng của người hiến', icon: <FaRegSmile />, color: 'from-green-500 to-green-600' },
  ];

  // Testimonials Data
  const testimonials = [
    {
      name: 'Nguyễn Văn An',
      role: 'Người hiến máu thường xuyên',
      content: 'Tôi đã hiến máu tại đây 15 lần. Đội ngũ y tế rất chuyên nghiệp và chu đáo. Cảm ơn bệnh viện đã tạo điều kiện tốt nhất.',
      rating: 5
    },
    {
      name: 'Trần Thị Bình',
      role: 'Nhân viên văn phòng',
      content: 'Lần đầu hiến máu tôi rất lo lắng, nhưng các bác sĩ đã tư vấn rất kỹ. Quy trình nhanh gọn và an toàn.',
      rating: 5
    },
    {
      name: 'Lê Minh Cường',
      role: 'Sinh viên đại học',
      content: 'Hiến máu là việc làm ý nghĩa. Tại đây tôi cảm thấy được tôn trọng và chăm sóc chu đáo.',
      rating: 5
    }
  ];

  if (isLoading) {
    return (
      <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="text-center">
          <motion.div
            animate={{ rotate: 360 }}
            transition={{ duration: 1, repeat: Infinity, ease: "linear" }}
            className="w-16 h-16 border-4 border-red-500 border-t-transparent rounded-full mx-auto mb-4"
          />
          <h2 className="text-2xl font-bold text-gray-800 mb-2">Đang tải...</h2>
          <p className="text-gray-600">Vui lòng chờ trong giây lát</p>
        </div>
      </div>
    );
  }

  return (
    <div className={`font-sans min-h-screen transition-colors duration-300 ${darkMode ? 'dark bg-gray-900' : 'bg-gray-50'}`}>
      {/* Enhanced Top Banner */}
      <motion.div
        initial={{ y: -50 }}
        animate={{ y: 0 }}
        className="bg-gradient-to-r from-red-600 via-red-500 to-pink-500 text-white text-center py-3 text-sm md:text-base relative overflow-hidden"
      >
        <motion.div
          animate={{ x: [-1000, 1000] }}
          transition={{ duration: 20, repeat: Infinity, ease: "linear" }}
          className="absolute inset-0 bg-gradient-to-r from-transparent via-white/10 to-transparent"
        />
        <p className="relative z-10">
          <FaHeartbeat className="inline mr-2" />
          HOTLINE HIẾN MÁU: <span className="font-bold">{hospitalInfo.hotline}</span> - Làm việc: {hospitalInfo.workingHours}
        </p>
      </motion.div>

      {/* Enhanced Header */}
      <motion.header
        initial={{ y: -100 }}
        animate={{ y: 0 }}
        className={`shadow-lg sticky top-0 z-50 backdrop-blur-md transition-colors duration-300 ${
          darkMode ? 'bg-gray-800/90' : 'bg-white/90'
        }`}
      >
        <div className="container mx-auto px-4 py-4 flex justify-between items-center">
          <motion.div
            whileHover={{ scale: 1.05 }}
            className="flex items-center space-x-4"
          >
            <motion.div
              whileHover={{ rotate: 360 }}
              transition={{ duration: 0.6 }}
              className="bg-gradient-to-r from-red-500 to-pink-500 p-3 rounded-full shadow-lg"
            >
              <FaHeartbeat className="text-2xl text-white" />
            </motion.div>
            <div>
              <h1 className={`text-xl font-bold ${darkMode ? 'text-white' : 'text-gray-800'}`}>
                HIẾN MÁU NHÂN ĐẠO
              </h1>
              <p className={`text-xs ${darkMode ? 'text-gray-300' : 'text-gray-600'}`}>
                {hospitalInfo.shortName}
              </p>
            </div>
          </motion.div>

          <div className="hidden md:flex items-center space-x-6">
            <motion.button
              whileHover={{ scale: 1.05 }}
              onClick={toggleDarkMode}
              className={`p-2 rounded-full transition-colors ${
                darkMode ? 'bg-gray-700 text-yellow-400' : 'bg-gray-100 text-gray-700'
              }`}
            >
              {darkMode ? <FaSun /> : <FaMoon />}
            </motion.button>
            
            <motion.button
              onClick={scrollToContact}
              whileHover={{ scale: 1.05 }}
              className={`flex items-center space-x-2 transition-colors ${
                darkMode ? 'text-gray-300 hover:text-red-400' : 'text-gray-700 hover:text-red-500'
              }`}
            >
              <FaMapMarkerAlt />
              <span>Địa chỉ</span>
            </motion.button>
            
            <motion.button
              whileHover={{ scale: 1.05, boxShadow: "0 10px 25px rgba(239, 68, 68, 0.3)" }}
              whileTap={{ scale: 0.95 }}
              className="bg-gradient-to-r from-red-500 to-pink-500 hover:from-red-600 hover:to-pink-600 text-white px-6 py-3 rounded-full transition-all duration-300 shadow-lg"
              onClick={showModal}
            >
              Đăng ký hiến máu
            </motion.button>
          </div>

          <button
            className={`md:hidden ${darkMode ? 'text-white' : 'text-gray-700'}`}
            onClick={() => setIsMenuOpen(!isMenuOpen)}
          >
            <motion.div
              animate={{ rotate: isMenuOpen ? 180 : 0 }}
              transition={{ duration: 0.3 }}
            >
              {isMenuOpen ? <FaTimes size={24} /> : <FaBars size={24} />}
            </motion.div>
          </button>
        </div>

        {/* Enhanced Mobile Menu */}
        <AnimatePresence>
          {isMenuOpen && (
            <motion.div
              initial={{ opacity: 0, height: 0 }}
              animate={{ opacity: 1, height: 'auto' }}
              exit={{ opacity: 0, height: 0 }}
              transition={{ duration: 0.3 }}
              className={`md:hidden shadow-lg ${darkMode ? 'bg-gray-800' : 'bg-white'}`}
            >
              <div className="container mx-auto px-4 py-4 flex flex-col space-y-4">
                <motion.button
                  whileHover={{ x: 10 }}
                  onClick={toggleDarkMode}
                  className={`flex items-center space-x-2 py-2 ${
                    darkMode ? 'text-gray-300' : 'text-gray-700'
                  }`}
                >
                  {darkMode ? <FaSun /> : <FaMoon />}
                  <span>{darkMode ? 'Chế độ sáng' : 'Chế độ tối'}</span>
                </motion.button>
                
                <motion.button
                  whileHover={{ x: 10 }}
                  onClick={scrollToContact}
                  className={`flex items-center space-x-2 py-2 ${
                    darkMode ? 'text-gray-300 hover:text-red-400' : 'text-gray-700 hover:text-red-500'
                  }`}
                >
                  <FaMapMarkerAlt />
                  <span>Địa chỉ</span>
                </motion.button>
                
                <motion.button
                  whileHover={{ scale: 1.02 }}
                  className="bg-gradient-to-r from-red-500 to-pink-500 text-white px-4 py-3 rounded-lg"
                  onClick={showModal}
                >
                  Đăng ký hiến máu
                </motion.button>
              </div>
            </motion.div>
          )}
        </AnimatePresence>
      </motion.header>

      {/* Enhanced Hero Section */}
      <motion.section
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ duration: 1 }}
        className="relative overflow-hidden"
      >
        <div className="absolute inset-0 bg-gradient-to-r from-black/70 via-black/50 to-black/30 z-10" />
        
        <motion.div
  style={{ y: y1 }}
  className="w-full h-[80vh] max-h-[800px] min-h-[500px] overflow-hidden"
>
          <img
            src="https://images.unsplash.com/photo-1581595219315-a187dd40c322?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2070&q=80"
            alt="Blood donation"
            className="w-full h-full object-cover"
          />
        </motion.div>

        <motion.div
          style={{ y: y2 }}
          className="absolute inset-0 z-20 flex flex-col items-center justify-center text-center px-4 text-white"
        >
          <motion.div
            initial={{ scale: 0.8, opacity: 0 }}
            animate={{ scale: 1, opacity: 1 }}
            transition={{ delay: 0.2, duration: 0.8 }}
            className="mb-6"
          >
            <motion.div
              animate={{
                scale: [1, 1.1, 1],
                rotate: [0, 5, -5, 0]
              }}
              transition={{
                duration: 3,
                repeat: Infinity,
                ease: "easeInOut"
              }}
              className="w-20 h-20 bg-red-500 rounded-full flex items-center justify-center mx-auto mb-4"
            >
              <FaHeartbeat className="text-3xl" />
            </motion.div>
          </motion.div>

          <motion.h1
            initial={{ y: -50, opacity: 0 }}
            animate={{ y: 0, opacity: 1 }}
            transition={{ delay: 0.4, duration: 0.8 }}
            className="text-4xl sm:text-5xl md:text-6xl font-bold mb-6 leading-tight"
          >
            <span className="bg-gradient-to-r from-red-400 to-pink-400 bg-clip-text text-transparent">
              MỘT GIỌT MÁU CHO ĐI
            </span>
            <br />
            <span className="text-white">MỘT CUỘC ĐỜI Ở LẠI</span>
          </motion.h1>

          <motion.p
            initial={{ y: 50, opacity: 0 }}
            animate={{ y: 0, opacity: 1 }}
            transition={{ delay: 0.6, duration: 0.8 }}
            className="text-xl md:text-2xl mb-10 max-w-3xl leading-relaxed"
          >
            Hiến máu cứu người - Nghĩa cử cao đẹp tại {hospitalInfo.name}
          </motion.p>

          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.8, duration: 0.8 }}
            className="flex flex-col sm:flex-row gap-6"
          >
            <motion.button
              whileHover={{
                scale: 1.05,
                boxShadow: "0 20px 40px rgba(239, 68, 68, 0.4)"
              }}
              whileTap={{ scale: 0.95 }}
              className="bg-gradient-to-r from-red-500 to-pink-500 hover:from-red-600 hover:to-pink-600 text-white px-8 py-4 rounded-full shadow-2xl text-lg font-semibold"
              onClick={showModal}
            >
              <FaHeartbeat className="inline mr-2" />
              Đăng ký hiến máu ngay
            </motion.button>
            
            <motion.button
              whileHover={{
                scale: 1.05,
                backgroundColor: "rgba(255, 255, 255, 0.95)"
              }}
              whileTap={{ scale: 0.95 }}
              className="bg-white/80 hover:bg-white/90 text-red-500 px-8 py-4 rounded-full shadow-2xl text-lg font-semibold backdrop-blur-sm"
              onClick={scrollToContact}
            >
              <FaMapMarkerAlt className="inline mr-2" />
              Xem địa chỉ hiến máu
            </motion.button>
          </motion.div>
        </motion.div>

        {/* Floating Elements */}
        <motion.div
          animate={{
            y: [0, -20, 0],
            rotate: [0, 5, -5, 0]
          }}
          transition={{ 
            duration: 4,
            repeat: Infinity,
            ease: "easeInOut"
          }}
          className="absolute top-20 left-10 w-16 h-16 bg-red-500/20 rounded-full blur-xl"
        />
        <motion.div
          animate={{ 
            y: [0, 20, 0],
            rotate: [0, -5, 5, 0]
          }}
          transition={{ 
            duration: 3,
            repeat: Infinity,
            ease: "easeInOut",
            delay: 1
          }}
          className="absolute bottom-20 right-10 w-20 h-20 bg-pink-500/20 rounded-full blur-xl"
        />
      </motion.section>

      {/* Rest of the component continues with enhanced sections... */}
      {/* I'll continue with the remaining sections in the next part */}
      
      {/* Enhanced About Section */}
      <section className={`py-20 transition-colors duration-300 ${darkMode ? 'bg-gray-800' : 'bg-white'}`}>
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
            className="text-center mb-16"
          >
            <h2 className={`text-4xl font-bold mb-6 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
              VỀ TRUNG TÂM HIẾN MÁU
            </h2>
            <div className="w-24 h-1 bg-gradient-to-r from-red-500 to-pink-500 mx-auto mb-6 rounded-full" />
            <p className={`max-w-4xl mx-auto text-lg leading-relaxed ${darkMode ? 'text-gray-300' : 'text-gray-600'}`}>
              Trung tâm Hiến máu thuộc {hospitalInfo.name} là đơn vị uy tín với hơn 15  năm kinh nghiệm, phục vụ trực tiếp nhu cầu điều trị cho hàng ngàn bệnh nhân mỗi năm.
            </p>
          </motion.div>

          <div className="grid lg:grid-cols-2 gap-12 items-center">
            <motion.div
              initial={{ opacity: 0, x: -50 }}
              whileInView={{ opacity: 1, x: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.8 }}
            >
              <div className="relative">
                <img
                  src="https://images.unsplash.com/photo-1576091160550-2173dba999ef?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80"
                  alt="Blood donation center"
                  className="rounded-2xl shadow-2xl w-full"
                />
                <div className="absolute inset-0 bg-gradient-to-t from-red-500/20 to-transparent rounded-2xl" />
                <motion.div
                  animate={{ scale: [1, 1.1, 1] }}
                  transition={{ duration: 2, repeat: Infinity }}
                  className="absolute top-4 right-4 bg-red-500 text-white p-3 rounded-full shadow-lg"
                >
                  <FaHeartbeat className="text-xl" />
                </motion.div>
              </div>
            </motion.div>

            <motion.div
              initial={{ opacity: 0, x: 50 }}
              whileInView={{ opacity: 1, x: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.8, delay: 0.2 }}
            >
              <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
                {[
                  {
                    icon: <FaRegCalendarAlt />,
                    title: 'Thời gian làm việc',
                    content: hospitalInfo.workingHours,
                    color: 'from-blue-500 to-blue-600'
                  },
                  {
                    icon: <FaUsers />,
                    title: 'Năng lực tiếp nhận',
                    content: hospitalInfo.capacity,
                    color: 'from-green-500 to-green-600'
                  },
                  {
                    icon: <FaPhone />,
                    title: 'Liên hệ',
                    content: `${hospitalInfo.phone}\nHotline: ${hospitalInfo.hotline}`,
                    color: 'from-purple-500 to-purple-600'
                  },
                  {
                    icon: <FaMapMarkerAlt />,
                    title: 'Địa chỉ',
                    content: hospitalInfo.address,
                    color: 'from-red-500 to-red-600'
                  }
                ].map((item, index) => (
                  <motion.div
                    key={index}
                    initial={{ opacity: 0, y: 30 }}
                    whileInView={{ opacity: 1, y: 0 }}
                    viewport={{ once: true }}
                    transition={{ duration: 0.6, delay: index * 0.1 }}
                    whileHover={{ y: -5, scale: 1.02 }}
                    className={`${darkMode ? 'bg-gray-700' : 'bg-gray-50'} p-6 rounded-2xl shadow-lg hover:shadow-xl transition-all duration-300`}
                  >
                    <div className={`w-12 h-12 bg-gradient-to-r ${item.color} rounded-full flex items-center justify-center mb-4 text-white text-xl`}>
                      {item.icon}
                    </div>
                    <h3 className={`font-semibold text-lg mb-3 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
                      {item.title}
                    </h3>
                    <p className={`leading-relaxed ${darkMode ? 'text-gray-300' : 'text-gray-600'}`}>
                      {item.content}
                    </p>
                  </motion.div>
                ))}
              </div>
            </motion.div>
          </div>
        </div>
      </section>

      {/* Enhanced Features Section */}
      <section className={`py-20 transition-colors duration-300 ${darkMode ? 'bg-gray-900' : 'bg-gray-50'}`}>
  <div className="w-full max-w-6xl mx-auto px-4">
    {/* Tiêu đề */}
    <motion.div
      initial={{ opacity: 0, y: 50 }}
      whileInView={{ opacity: 1, y: 0 }}
      viewport={{ once: true }}
      transition={{ duration: 0.8 }}
      className="text-center mb-16"
    >
      <h2 className={`text-3xl md:text-4xl font-bold mb-6 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
        TẠI SAO NÊN HIẾN MÁU TẠI ĐÂY?
      </h2>
      <div className="w-24 h-1 bg-gradient-to-r from-red-500 to-pink-500 mx-auto mb-6 rounded-full" />
      <p className={`max-w-3xl mx-auto text-base md:text-lg leading-relaxed ${darkMode ? 'text-gray-300' : 'text-gray-600'}`}>
        Chúng tôi cam kết mang đến trải nghiệm hiến máu an toàn, thoải mái và ý nghĩa nhất cho bạn.
      </p>
    </motion.div>

    {/* Danh sách các feature */}
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 xl:grid-cols-4 gap-4">
  {features.map((feature, index) => (
    <div className="bg-white p-4 md:p-5 rounded-2xl shadow-md border transition-transform duration-300 hover:scale-105">
      <FeatureCard
        key={index}
        icon={feature.icon}
        title={feature.title}
        description={feature.description}
        color={feature.color}
        delay={index * 0.2}
      />
    </div>
  ))}
</div>
  </div>
</section>


      {/* Enhanced Requirements Section */}
      <section className={`py-20 transition-colors duration-300 ${darkMode ? 'bg-gray-800' : 'bg-white'}`}>
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
            className="text-center mb-16"
          >
            <h2 className={`text-4xl font-bold mb-6 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
              ĐIỀU KIỆN HIẾN MÁU
            </h2>
            <div className="w-24 h-1 bg-gradient-to-r from-red-500 to-pink-500 mx-auto mb-6 rounded-full" />
            <p className={`max-w-4xl mx-auto text-lg leading-relaxed ${darkMode ? 'text-gray-300' : 'text-gray-600'}`}>
              Để đảm bảo an toàn cho người hiến máu và người nhận máu, vui lòng kiểm tra các điều kiện sau:
            </p>
          </motion.div>

          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8 mb-12">
            {requirements.map((req, index) => (
              <motion.div
                key={index}
                initial={{ opacity: 0, y: 50 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true }}
                transition={{ duration: 0.6, delay: index * 0.1 }}
                whileHover={{ y: -8, scale: 1.02 }}
                className={`${darkMode ? 'bg-gray-700' : 'bg-gray-50'} p-8 rounded-2xl text-center shadow-lg hover:shadow-xl transition-all duration-300 group`}
              >
                <motion.div
                  whileHover={{ scale: 1.2, rotate: 10 }}
                  className="flex justify-center mb-6"
                >
                  <div className="bg-gradient-to-r from-red-500 to-pink-500 p-4 rounded-full text-white text-2xl group-hover:shadow-lg transition-shadow">
                    {req.icon}
                  </div>
                </motion.div>
                <h3 className={`text-xl font-semibold mb-4 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
                  {req.title}
                </h3>
                <p className={`leading-relaxed ${darkMode ? 'text-gray-300' : 'text-gray-600'}`}>
                  {req.description}
                </p>
              </motion.div>
            ))}
          </div>

          <motion.div
            initial={{ opacity: 0, y: 30 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
            className={`${darkMode ? 'bg-red-900/20 border-red-400' : 'bg-red-50 border-red-500'} border-l-4 p-8 rounded-r-2xl shadow-lg`}
          >
            <h3 className={`text-2xl font-semibold mb-6 flex items-center ${darkMode ? 'text-white' : 'text-gray-800'}`}>
              <motion.div
                animate={{ scale: [1, 1.2, 1] }}
                transition={{ duration: 2, repeat: Infinity }}
                className="mr-3"
              >
                <FaCheckCircle className="text-red-500" />
              </motion.div>
              Lưu ý quan trọng
            </h3>
            <div className="grid md:grid-cols-2 gap-4">
              {[
                'Không hiến máu khi đang đói hoặc mệt mỏi',
                'Không uống rượu bia trong vòng 24 giờ trước khi hiến máu',
                'Ngủ đủ giấc trước ngày hiến máu',
                'Uống nhiều nước trước và sau khi hiến máu'
              ].map((note, index) => (
                <motion.div
                  key={index}
                  initial={{ opacity: 0, x: -20 }}
                  whileInView={{ opacity: 1, x: 0 }}
                  viewport={{ once: true }}
                  transition={{ duration: 0.5, delay: index * 0.1 }}
                  className="flex items-start space-x-3"
                >
                  <FaCheckCircle className="text-red-500 mt-1 flex-shrink-0" />
                  <span className={`${darkMode ? 'text-gray-300' : 'text-gray-700'}`}>
                    {note}
                  </span>
                </motion.div>
              ))}
            </div>
          </motion.div>
        </div>
      </section>

      {/* Enhanced Process Section */}
      <section className={`py-20 transition-colors duration-300 ${darkMode ? 'bg-gray-900' : 'bg-gray-50'}`}>
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
            className="text-center mb-16"
          >
            <h2 className={`text-4xl font-bold mb-6 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
              QUY TRÌNH HIẾN MÁU
            </h2>
            <div className="w-24 h-1 bg-gradient-to-r from-red-500 to-pink-500 mx-auto mb-6 rounded-full" />
            <p className={`max-w-4xl mx-auto text-lg leading-relaxed ${darkMode ? 'text-gray-300' : 'text-gray-600'}`}>
              Quy trình hiến máu đơn giản, nhanh chóng và an toàn tại {hospitalInfo.name}
            </p>
          </motion.div>

          <div className="max-w-4xl mx-auto mb-12">
            {processSteps.map((step, index) => (
              <ProcessStep
                key={index}
                number={step.number}
                title={step.title}
                description={step.description}
                icon={step.icon}
                isActive={activeStep === parseInt(step.number)}
              />
            ))}
          </div>

          <motion.div
            initial={{ opacity: 0, y: 30 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
            className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6"
          >
            {[
              { label: 'Đăng ký & Khám sàng lọc', time: '15-20 phút', icon: <FaIdCard />, color: 'from-blue-500 to-blue-600' },
              { label: 'Hiến máu', time: '5-10 phút', icon: <FaTint />, color: 'from-red-500 to-red-600' },
              { label: 'Nghỉ ngơi sau hiến', time: '15-20 phút', icon: <FaRegClock />, color: 'from-green-500 to-green-600' },
              { label: 'Tổng thời gian', time: '45-60 phút', icon: <FaClock />, color: 'from-purple-500 to-purple-600' }
            ].map((item, index) => (
              <motion.div
                key={index}
                initial={{ opacity: 0, scale: 0.9 }}
                whileInView={{ opacity: 1, scale: 1 }}
                viewport={{ once: true }}
                transition={{ duration: 0.5, delay: index * 0.1 }}
                whileHover={{ y: -5, scale: 1.05 }}
                className={`${darkMode ? 'bg-gray-800' : 'bg-white'} p-6 rounded-2xl shadow-lg hover:shadow-xl transition-all duration-300 text-center group`}
              >
                <div className={`w-12 h-12 bg-gradient-to-r ${item.color} rounded-full flex items-center justify-center mx-auto mb-4 text-white group-hover:scale-110 transition-transform`}>
                  {item.icon}
                </div>
                <p className={`font-semibold mb-2 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
                  {item.label}
                </p>
                <p className="text-red-500 font-bold text-lg">{item.time}</p>
              </motion.div>
            ))}
          </motion.div>
        </div>
      </section>

      {/* Enhanced Stats Section */}
      <section className="py-16 md:py-20 bg-gradient-to-r from-red-600 via-red-500 to-pink-500">
        <div className="absolute inset-0">
          <motion.div
            animate={{ 
              scale: [1, 1.2, 1],
              rotate: [0, 180, 360]
            }}
            transition={{ 
              duration: 20,
              repeat: Infinity,
              ease: "linear"
            }}
            className="absolute top-10 left-10 w-32 h-32 bg-white/5 rounded-full"
          />
          <motion.div
            animate={{ 
              scale: [1.2, 1, 1.2],
              rotate: [360, 180, 0]
            }}
            transition={{ 
              duration: 15,
              repeat: Infinity,
              ease: "linear"
            }}
            className="absolute bottom-10 right-10 w-24 h-24 bg-white/5 rounded-full"
          />
        </div>

        <div className="container mx-auto px-4 relative z-10">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
            className="text-center mb-16"
          >
            <h2 className="text-4xl font-bold mb-6">THÀNH TỰU CỦA CHÚNG TÔI</h2>
            <div className="w-24 h-1 bg-white mx-auto mb-6 rounded-full" />
            <p className="max-w-4xl mx-auto text-lg leading-relaxed opacity-90">
              Những con số biết nói về hành trình hiến máu nhân đạo tại {hospitalInfo.name}
            </p>
          </motion.div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            {stats.map((stat, index) => (
              <motion.div
                key={index}
                initial={{ y: 100, opacity: 0 }}
                whileInView={{ y: 0, opacity: 1 }}
                viewport={{ once: true }}
                transition={{ duration: 0.8, delay: index * 0.2 }}
                whileHover={{ scale: 1.05, y: -10 }}
                className="text-center group"
              >
                <motion.div
                  whileHover={{ rotate: 360 }}
                  transition={{ duration: 0.6 }}
                  className={`bg-gradient-to-r ${stat.color} p-8 rounded-full inline-block mb-6 shadow-2xl group-hover:shadow-3xl transition-shadow`}
                >
                  {React.cloneElement(stat.icon, { className: "text-4xl" })}
                </motion.div>
                <motion.h3
                  initial={{ scale: 0 }}
                  whileInView={{ scale: 1 }}
                  viewport={{ once: true }}
                  transition={{ duration: 0.6, delay: index * 0.2 + 0.3 }}
                  className="text-5xl font-bold mb-4 group-hover:text-yellow-300 transition-colors"
                >
                  {stat.value}
                </motion.h3>
                <p className="text-xl opacity-90 group-hover:opacity-100 transition-opacity">
                  {stat.label}
                </p>
              </motion.div>
            ))}
          </div>
        </div>
      </section>

      {/* New Testimonials Section */}
      <section className={`py-20 transition-colors duration-300 ${darkMode ? 'bg-gray-800' : 'bg-white'}`}>
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
            className="text-center mb-16"
          >
            <h2 className={`text-4xl font-bold mb-6 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
              CẢM NHẬN CỦA NGƯỜI HIẾN MÁU
            </h2>
            <div className="w-24 h-1 bg-gradient-to-r from-red-500 to-pink-500 mx-auto mb-6 rounded-full" />
            <p className={`max-w-4xl mx-auto text-lg leading-relaxed ${darkMode ? 'text-gray-300' : 'text-gray-600'}`}>
              Những chia sẻ chân thành từ những người đã tham gia hiến máu tại trung tâm của chúng tôi
            </p>
          </motion.div>

          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {testimonials.map((testimonial, index) => (
              <TestimonialCard
                key={index}
                name={testimonial.name}
                role={testimonial.role}
                content={testimonial.content}
                rating={testimonial.rating}
                delay={index * 0.2}
              />
            ))}
          </div>
        </div>
      </section>

      {/* Enhanced Contact Section */}
      <section ref={contactRef} className={`py-20 transition-colors duration-300 ${darkMode ? 'bg-gray-900' : 'bg-gray-50'}`}>
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
            className="text-center mb-16"
          >
            <h2 className={`text-4xl font-bold mb-6 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
              LIÊN HỆ CHÚNG TÔI
            </h2>
            <div className="w-24 h-1 bg-gradient-to-r from-red-500 to-pink-500 mx-auto mb-6 rounded-full" />
            <p className={`max-w-4xl mx-auto text-lg leading-relaxed ${darkMode ? 'text-gray-300' : 'text-gray-600'}`}>
              Mọi thắc mắc về hiến máu, vui lòng liên hệ với chúng tôi
            </p>
          </motion.div>

          <div className="grid lg:grid-cols-2 gap-12">
            <motion.div
              initial={{ opacity: 0, x: -50 }}
              whileInView={{ opacity: 1, x: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.8 }}
              className={`${darkMode ? 'bg-gray-800' : 'bg-white'} p-10 rounded-2xl shadow-xl`}
            >
              <h3 className={`text-2xl font-semibold mb-8 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
                Thông tin liên hệ
              </h3>
              
              <div className="space-y-6">
                {[
                  {
                    icon: <FaMapMarkerAlt />,
                    title: 'Địa chỉ:',
                    content: hospitalInfo.address,
                    color: 'from-red-500 to-red-600'
                  },
                  {
                    icon: <FaPhone />,
                    title: 'Điện thoại:',
                    content: hospitalInfo.phone,
                    color: 'from-blue-500 to-blue-600'
                  },
                  {
                    icon: <FaPhone />,
                    title: 'Hotline:',
                    content: hospitalInfo.hotline,
                    color: 'from-green-500 to-green-600'
                  },
                  {
                    icon: <FaRegClock />,
                    title: 'Thời gian làm việc:',
                    content: hospitalInfo.workingHours,
                    color: 'from-purple-500 to-purple-600'
                  }
                ].map((item, index) => (
                  <motion.div
                    key={index}
                    initial={{ opacity: 0, y: 20 }}
                    whileInView={{ opacity: 1, y: 0 }}
                    viewport={{ once: true }}
                    transition={{ duration: 0.5, delay: index * 0.1 }}
                    className="flex items-start space-x-4 group"
                  >
                    <div className={`w-12 h-12 bg-gradient-to-r ${item.color} rounded-full flex items-center justify-center text-white text-xl group-hover:scale-110 transition-transform`}>
                      {item.icon}
                    </div>
                    <div>
                      <h4 className={`font-medium mb-1 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
                        {item.title}
                      </h4>
                      <p className={`leading-relaxed ${darkMode ? 'text-gray-300' : 'text-gray-600'}`}>
                        {item.content}
                      </p>
                    </div>
                  </motion.div>
                ))}
              </div>

              <div className="mt-10">
                <h4 className={`font-medium mb-6 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
                  Kết nối với chúng tôi:
                </h4>
                <div className="flex space-x-4">
                  {[
                    { icon: <FaFacebook />, href: 'https://facebook.com', color: 'hover:bg-blue-600' },
                    { icon: <FaTwitter />, href: 'https://twitter.com', color: 'hover:bg-blue-400' },
                    { icon: <FaInstagram />, href: 'https://instagram.com', color: 'hover:bg-pink-600' }
                  ].map((social, index) => (
                    <motion.a
                      key={index}
                      href={social.href}
                      whileHover={{ scale: 1.1, y: -2 }}
                      whileTap={{ scale: 0.95 }}
                      className={`${darkMode ? 'bg-gray-700 hover:bg-gray-600' : 'bg-gray-200 hover:bg-gray-300'} ${social.color} p-4 rounded-full text-gray-700 hover:text-white transition-all duration-300 shadow-lg hover:shadow-xl`}
                    >
                      {React.cloneElement(social.icon, { className: "text-xl" })}
                    </motion.a>
                  ))}
                </div>
              </div>
            </motion.div>

            <motion.div
              initial={{ opacity: 0, x: 50 }}
              whileInView={{ opacity: 1, x: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.8, delay: 0.2 }}
              className={`${darkMode ? 'bg-gray-800' : 'bg-white'} p-10 rounded-2xl shadow-xl`}
            >
              <h3 className={`text-2xl font-semibold mb-8 ${darkMode ? 'text-white' : 'text-gray-800'}`}>
                Bản đồ
              </h3>
              <div className="aspect-w-16 aspect-h-9 bg-gray-200 rounded-xl overflow-hidden shadow-inner">
                <iframe
                  src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.126365377253!2d106.7042763152607!3d10.801834461698787!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x317528b2747a81a3%3A0x33b2a2e2409e40e1!2s125%20L%C3%AA%20L%E1%BB%A3i%2C%20B%E1%BA%BFn%20Ngh%C3%A9%2C%20Qu%E1%BA%ADn%201%2C%20Th%C3%A0nh%20ph%E1%BB%91%20H%E1%BB%93%20Ch%C3%AD%20Minh%2C%20Vietnam!5e0!3m2!1sen!2s!4v1620000000000!5m2!1sen!2s"
                  width="100%"
                  height="100%"
                  style={{ border: 0 }}
                  allowFullScreen=""
                  loading="lazy"
                  className="min-h-[400px] rounded-xl"
                ></iframe>
              </div>
            </motion.div>
          </div>
        </div>
      </section>

      {/* Enhanced CTA Section */}
      <section className="py-20 bg-gradient-to-r from-red-600 via-red-500 to-pink-500 text-white relative overflow-hidden">
        <div className="absolute inset-0">
          <motion.div
            animate={{ 
              rotate: [0, 360],
              scale: [1, 1.1, 1]
            }}
            transition={{ 
              duration: 10,
              repeat: Infinity,
              ease: "linear"
            }}
            className="absolute top-0 left-0 w-full h-full bg-gradient-to-r from-transparent via-white/5 to-transparent"
          />
        </div>

        <div className="container mx-auto px-4 text-center relative z-10">
          <motion.div
            initial={{ opacity: 0, scale: 0.9 }}
            whileInView={{ opacity: 1, scale: 1 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
          >
            <motion.div
              animate={{ 
                scale: [1, 1.1, 1],
                  rotate: [0, 5, -5, 0]
              }}
              transition={{ 
                duration: 3,
                repeat: Infinity,
                ease: "easeInOut"
              }}
              className="w-24 h-24 bg-white/20 rounded-full flex items-center justify-center mx-auto mb-8"
            >
              <FaHeartbeat className="text-4xl" />
            </motion.div>

            <h2 className="text-4xl md:text-5xl font-bold mb-8">
              SẴN SÀNG HIẾN MÁU CỨU NGƯỜI?
            </h2>
            <p className="text-xl md:text-2xl mb-10 max-w-3xl mx-auto leading-relaxed opacity-90">
              Đăng ký ngay hôm nay để trở thành người hùng thầm lặng cứu sống nhiều bệnh nhân
            </p>
            
            <div className="flex flex-col sm:flex-row gap-6 justify-center items-center">
              <motion.button
                whileHover={{ 
                  scale: 1.05,
                  boxShadow: "0 20px 40px rgba(255, 255, 255, 0.3)"
                }}
                whileTap={{ scale: 0.95 }}
                className="bg-white hover:bg-gray-100 text-red-600 px-10 py-5 text-xl font-semibold rounded-full shadow-2xl transition-all duration-300 flex items-center space-x-3"
                onClick={showModal}
              >
                <FaHeartbeat className="text-2xl" />
                <span>Đăng ký hiến máu ngay</span>
                <FaArrowRight className="text-lg" />
              </motion.button>
              
              <motion.button
                whileHover={{ scale: 1.05 }}
                whileTap={{ scale: 0.95 }}
                className="border-2 border-white text-white hover:bg-white hover:text-red-600 px-8 py-4 text-lg font-semibold rounded-full transition-all duration-300"
                onClick={scrollToContact}
              >
                Tìm hiểu thêm
              </motion.button>
            </div>
          </motion.div>
        </div>
      </section>

      {/* Enhanced Back to Top Button */}
      <motion.button
        style={{ opacity: showBackToTop }}
        onClick={scrollToTop}
        whileHover={{ 
          scale: 1.1,
          boxShadow: "0 10px 25px rgba(239, 68, 68, 0.4)"
        }}
        whileTap={{ scale: 0.9 }}
        className="fixed bottom-8 right-8 bg-gradient-to-r from-red-500 to-pink-500 text-white p-4 rounded-full shadow-2xl hover:shadow-3xl z-50 transition-all duration-300"
      >
        <FaArrowUp size={24} />
      </motion.button>

      {/* Enhanced Footer */}
      <footer className={`py-12 transition-colors duration-300 ${darkMode ? 'bg-gray-900' : 'bg-gray-800'} text-white`}>
        <div className="container mx-auto px-4">
          <div className="grid md:grid-cols-3 gap-8 mb-8">
            <motion.div
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.6 }}
            >
              <div className="flex items-center space-x-4 mb-6">
                <motion.div
                  whileHover={{ rotate: 360 }}
                  transition={{ duration: 0.6 }}
                  className="bg-gradient-to-r from-red-500 to-pink-500 p-3 rounded-full"
                >
                  <FaHeartbeat className="text-2xl" />
                </motion.div>
                <div>
                  <h3 className="text-xl font-bold">TRUNG TÂM HIẾN MÁU</h3>
                  <p className="text-gray-400 text-sm">{hospitalInfo.shortName}</p>
                </div>
              </div>
              <p className="text-gray-400 leading-relaxed">
                Chúng tôi cam kết mang đến dịch vụ hiến máu an toàn, chuyên nghiệp và ý nghĩa nhất.
              </p>
            </motion.div>

            <motion.div
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.6, delay: 0.1 }}
            >
              <h4 className="text-lg font-semibold mb-6">Liên kết nhanh</h4>
              <div className="space-y-3">
                {[
                  { label: 'Về chúng tôi', action: () => {} },
                  { label: 'Quy trình hiến máu', action: () => {} },
                  { label: 'Điều kiện hiến máu', action: () => {} },
                  { label: 'Liên hệ', action: scrollToContact }
                ].map((link, index) => (
                  <motion.button
                    key={index}
                    whileHover={{ x: 5 }}
                    onClick={link.action}
                    className="block text-gray-400 hover:text-white transition-colors"
                  >
                    {link.label}
                  </motion.button>
                ))}
              </div>
            </motion.div>

            <motion.div
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.6, delay: 0.2 }}
            >
              <h4 className="text-lg font-semibold mb-6">Thông tin liên hệ</h4>
              <div className="space-y-3 text-gray-400">
                <p className="flex items-center space-x-2">
                  <FaMapMarkerAlt className="text-red-500" />
                  <span className="text-sm">{hospitalInfo.address}</span>
                </p>
                <p className="flex items-center space-x-2">
                  <FaPhone className="text-red-500" />
                  <span>{hospitalInfo.hotline}</span>
                </p>
                <p className="flex items-center space-x-2">
                  <FaRegClock className="text-red-500" />
                  <span className="text-sm">{hospitalInfo.workingHours}</span>
                </p>
              </div>
            </motion.div>
          </div>

          <motion.div
            initial={{ opacity: 0 }}
            whileInView={{ opacity: 1 }}
            viewport={{ once: true }}
            transition={{ duration: 0.6, delay: 0.3 }}
            className="border-t border-gray-700 pt-8 flex flex-col md:flex-row justify-between items-center"
          >
            <p className="text-gray-400 text-sm mb-4 md:mb-0">
              © {new Date().getFullYear()} {hospitalInfo.name}. All rights reserved.
            </p>
            <p className="text-gray-400 text-sm flex items-center">
              Thiết kế với <FaHeartbeat className="text-red-500 mx-1" /> vì cộng đồng
            </p>
          </motion.div>
        </div>
      </footer>

      {/* Enhanced Registration Modal */}
      <Modal
        open={open}
        title={
          <div className="flex items-center space-x-3">
            <div className="bg-gradient-to-r from-red-500 to-pink-500 p-2 rounded-full">
              <FaHeartbeat className="text-white" />
            </div>
            <span className="text-xl font-semibold">Đăng ký hiến máu</span>
          </div>
        }
        onOk={handleOk}
        onCancel={handleCancel}
        footer={null}
        centered
        className="rounded-2xl"
        width={600}
      >
        <div className="mt-6">
          <div className="bg-gradient-to-r from-red-50 to-pink-50 p-4 rounded-xl mb-6">
            <p className="text-gray-700 text-center">
              <FaCheckCircle className="inline text-red-500 mr-2" />
              Cảm ơn bạn đã quan tâm đến việc hiến máu cứu người!
            </p>
          </div>

          <Form
            name="registerForm"
            layout="vertical"
            onFinish={handleOk}
            className="space-y-4"
          >
            <div className="grid md:grid-cols-2 gap-4">
              <Form.Item
                label={<span className="font-medium">Họ và tên</span>}
                name="name"
                rules={[{ required: true, message: 'Vui lòng nhập họ tên!' }]}
              >
                <Input 
                  placeholder="Nhập họ và tên đầy đủ" 
                  className="rounded-lg h-12"
                  prefix={<FaUserMd className="text-gray-400" />}
                />
              </Form.Item>

              <Form.Item
                label={<span className="font-medium">Số điện thoại</span>}
                name="phone"
                rules={[{ required: true, message: 'Vui lòng nhập số điện thoại!' }]}
              >
                <Input 
                  placeholder="Nhập số điện thoại liên hệ" 
                  className="rounded-lg h-12"
                  prefix={<FaPhone className="text-gray-400" />}
                />
              </Form.Item>
            </div>

            <Form.Item
              label={<span className="font-medium">Email</span>}
              name="email"
              rules={[{ type: 'email', message: 'Email không hợp lệ!' }]}
            >
              <Input 
                placeholder="Nhập email (nếu có)" 
                className="rounded-lg h-12"
                prefix={<span className="text-gray-400">@</span>}
              />
            </Form.Item>

            <Form.Item
              label={<span className="font-medium">Ngày dự kiến hiến máu</span>}
              name="date"
              rules={[{ required: true, message: 'Vui lòng chọn ngày!' }]}
            >
              <Input 
                type="date" 
                className="rounded-lg h-12"
                prefix={<FaRegCalendarAlt className="text-gray-400" />}
              />
            </Form.Item>

            <div className="bg-yellow-50 border border-yellow-200 rounded-lg p-4 mb-6">
              <h4 className="font-medium text-yellow-800 mb-2 flex items-center">
                <FaCheckCircle className="mr-2" />
                Lưu ý trước khi hiến máu:
              </h4>
              <ul className="text-sm text-yellow-700 space-y-1">
                <li>• Ngủ đủ giấc và ăn uống đầy đủ trước khi đến</li>
                <li>• Mang theo CMND/CCCD hoặc giấy tờ tùy thân</li>
                <li>• Không uống rượu bia trong 24h trước khi hiến máu</li>
              </ul>
            </div>

            <Form.Item>
              <motion.div whileHover={{ scale: 1.02 }} whileTap={{ scale: 0.98 }}>
                <Button
                  type="primary"
                  htmlType="submit"
                  loading={loading}
                  block
                  className="rounded-lg h-12 font-medium text-lg bg-gradient-to-r from-red-500 to-pink-500 border-none hover:from-red-600 hover:to-pink-600"
                  icon={loading ? <FaSpinner className="animate-spin" /> : <FaHeartbeat />}
                >
                  {loading ? 'Đang gửi đăng ký...' : 'Gửi đăng ký'}
                </Button>
              </motion.div>
            </Form.Item>

            <div className="text-center text-sm text-gray-600">
              Bằng cách đăng ký, bạn đồng ý với{' '}
              <a href="#" className="text-red-500 hover:underline font-medium">
                Điều khoản sử dụng
              </a>{' '}
              của chúng tôi
            </div>
          </Form>
        </div>
      </Modal>
    </div>
  );
}

export default EnhancedHospitalBloodDonation;
